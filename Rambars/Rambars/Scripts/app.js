angular.module('rambars', [])
    .controller('BarsController', function ($scope, $http) {
        $http.get('/api/bars').
        then(function (response) {
            $scope.bars = response.data;
            $.each($scope.bars.Specials, function (item) {
                item.StartTime = $scope.parseDate(item.StartTime);
                item.EndTime = $scope.parseDate(item.EndTime);
            });
        });

        $scope.parseDate = function (dateStr) {
            return new Date(dateStr);
        }
    });