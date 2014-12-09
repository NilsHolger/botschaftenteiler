angular.module("myApp", ["ngRoute"])
    .config(function ($routeProvider) {
        $routeProvider.when("/", {
            controller: "TopicsCtrl",
            templateUrl: "/templates/Themen.html"
        });
        $routeProvider.when("/neuenachricht", {
            controller: "NewTopicsCtrl",
            templateUrl: "/templates/NeuesThema.html"
        });
        $routeProvider.when("/nachricht/:id", {
            controller: "SingleTopicCtrl",
            templateUrl: "/templates/Nachricht.html"
        });

        $routeProvider.otherwise({ redirectTo: "/" });
    })
    .controller("TopicsCtrl", function TopicsCtrl($scope, $http, DataService) {
        $scope.data = DataService;
        $scope.isBusy = false;

        if (DataService.isReady() == false) {
            $scope.isBusy = true;
            DataService.getTopics()
                .then(function () {

                },
                function () {
                    alert("Themen konnten nicht geladen werden.")
                })
            .then(function () {
                $scope.isBusy = false;
            });
        }

    }).controller("NewTopicsCtrl", function NewTopicsCtrl($scope, $http, $window, DataService) {
        $scope.newTopic = {};

        $scope.save = function () {
            DataService.addTopic($scope.newTopic)
            .then(function () {
                 $window.location = "#/";
                //$window.location.href = "http://localhost:63103/#/";
            },
            function () {
               alert("Neue Thema konnte nicht gespeichert werden.")
            });
            console.log(DataService.isReady());
            //$window.location = "#/";
        };

    })
    .controller("SingleTopicCtrl", function SingleTopicCtrl($scope, DataService, $window, $routeParams) {
        $scope.topic = null;
        $scope.newReply = {};

        DataService.getTopicById($routeParams.id).then(function (topic) {
            $scope.topic = topic;
        },
        function () {
            $window.location = "#/";
        });

        $scope.addReply = function () {
            DataService.saveReply($scope.topic, $scope.newReply)
     .then(function () {
         // success
         $scope.newReply.body = "";
     },
     function () {
         // error
         alert("Deine neue Antwort konnte nicht gespeichert werden.");
     });
        };
    })
    .factory("DataService", function ($http, $q) {

        var _topics = [];
        var _isInitialized = false;

        var _isReady = function () {
            return _isInitialized;
        }

        var _getTopics = function () {
            var deferred = $q.defer();
            $http.get("/api/v2/themen?mitAntworten=true")
       .then(function (result) {
           angular.copy(result.data, _topics);
           _isInitialized = true;
           deferred.resolve();
       },
       function () {
           deferred.reject();
       });
            return deferred.promise;
        };

        var _addTopic = function (newTopic) {
            var deferred = $q.defer();
            $http.post("/api/v2/themen", newTopic)
            .then(function (result) {
                var newCreatedTopic = result.data;
                _topics.splice(0, 0, newCreatedTopic);
                //_isInitialized = false;
                deferred.resolve(newCreatedTopic);
            },
    function () {
        deferred.reject();
    });
            return deferred.promise;
        };
        function _findTopic(id) {
            var found = null;

            $.each(_topics, function (i, item) {
                if (item.id == id) {
                    found = item;
                    return false;
                }
            });

            return found;
        }

        var _getTopicById = function (id) {
            var deferred = $q.defer();

            if (_isReady()) {
                var topic = _findTopic(id);
                if (topic) {
                    deferred.resolve(topic);
                } else {
                    deferred.reject();
                }
            } else {
                _getTopics().then(function () {
                    var topic = _findTopic(id);
                    if (topic) {
                        deferred.resolve(topic);
                    } else {
                        deferred.reject();
                    }
                },
                  function () {
                      deferred.reject();
                  });
            }
            return deferred.promise;
        };

        var _saveReply = function (topic, newReply) {
            var deferred = $q.defer();

            $http.post("/api/v2/themen/" + topic.id + "/antworten", newReply)
              .then(function (result) {
                  // success
                  if (topic.replies == null) topic.replies = [];
                  topic.replies.push(result.data);
                  deferred.resolve(result.data);
              },
              function () {
                  // error
                  deferred.reject();
              });
            return deferred.promise;
        };

        return {
            topics: _topics,
            getTopics: _getTopics,
            addTopic: _addTopic,
            isReady: _isReady,
            getTopicById: _getTopicById,
            saveReply: _saveReply
        };
    });