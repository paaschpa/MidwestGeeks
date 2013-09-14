var homeModule = angular.module('homeModule', []);

homeModule.controller('homeCtrl', ['$scope', '$http', 'dateFilter', function ($scope, $http, dateFilter) {

    $scope.dateFormat = function (d) {
        //should probably handle date on the backend...but meh, this hack works
        if (!d) {
            return "";
        }
        var dt = new Date(parseInt(d.substr(6)));
        return dateFilter(dt, 'EEE, MMM d');
    };

    $http.get('/api/upcomingmeetings')
        .success(function (data) {
            $scope.allMeetings = data;
            $scope.upcomingMeetings = data.slice(0, 4);
        })
         .error(function (data) {
             $scope.errorMessage = data.responseStatus.message;
         });
}]);
