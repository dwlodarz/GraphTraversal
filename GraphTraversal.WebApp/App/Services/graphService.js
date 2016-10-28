(function () {
    'use strict';
    angular.module('app.services', [])
        .service("graphService", ['$q', '$http', function ($q, $http) {
            var apiUri = 'http://localhost/GraphTraversal.WebServices/DomainSpecificService.svc/';

            function getShortestPath(startId, endId) {
                var getShortestPathRequest = $http({
                    method: 'GET',
                    url: apiUri + 'shortestPath/' + startId + '/' + endId
                }).then(function (response) {
                    if (response && response.data) {
                        return response.data;
                    }
                });
                return getShortestPathRequest;
            }
            return {
                GetShortestPath: getShortestPath,
            };
        }]);
})();