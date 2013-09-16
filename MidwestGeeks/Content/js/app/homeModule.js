var homeModule = angular.module('homeModule', []);

homeModule.controller('homeCtrl', ['$scope', '$http', 'dateFilter', function ($scope, $http, dateFilter) {
    $scope.pageIndex = 1;

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
            $scope.upcomingMeetings = data.slice(0, 5);
        })
         .error(function (data) {
             $scope.errorMessage = data.responseStatus.message;
         });

    $scope.nextMeetings = function () {
        $scope.pageIndex++;
        upcomingMeetingsChange();
    };

    $scope.previousMeetings = function () {
        $scope.pageIndex--;
        upcomingMeetingsChange();
    };
    
    $scope.register = function () {
        var newRegistration = {
            'username': $scope.userName,
            'email': $scope.email,
            'password': $scope.password,
            'autologin': true
        };

        var registration = $http.post('/api/register', newRegistration);
        registration.success(function (data) {
            $('#RegistrationForm').hide();
            $('#RegistrationSuccess').show();
        });
        registration.error(function (data) {
            $('#error').show();
            $('#RegistrationSuccess').hide();
            $scope.errorMsg = data.responseStatus.message;
        });
    };
    
    function upcomingMeetingsChange() {
        if ($scope.pageIndex < 1) {
            $scope.pageIndex++;
            return;
        }

        if ($scope.pageIndex > 6) {
            $scope.pageIndex--;
            return;
        }
        
        $scope.upcomingMeetings = $scope.allMeetings.slice(($scope.pageIndex - 1) * 5, $scope.pageIndex * 5);
    }

}]);
