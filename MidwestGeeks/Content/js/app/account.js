//var accountModule = angular.module('accountModule', ['ui.directives']);
var accountModule = angular.module('accountModule', ['ui.bootstrap']);

accountModule.controller('accountCtrl', ['$scope', '$http', 'dateFilter', function ($scope, $http, dateFilter) {
    $scope.meetingTime = new Date();
    $("#timeWrapper").hide();

    $scope.hstep = 1;
    $scope.mstep = 15;

    $scope.options = {
        hstep: [1, 2, 3],
        mstep: [1, 5, 10, 15, 25, 30]
    };

    $scope.ismeridian = true;
    $scope.toggleMode = function () {
        $scope.ismeridian = !$scope.ismeridian;
    };

    $scope.update = function () {
        var d = new Date();
        d.setHours(14);
        d.setMinutes(0);
        $scope.meetingTime = d;
    };

    $scope.clear = function () {
        $scope.mytime = null;
    };

    $scope.showTime = function () {
        $('#timeWrapper').show();
    }
}]);