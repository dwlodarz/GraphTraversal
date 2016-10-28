(function () {
    'use strict';
    angular.module('app.services', [])
        .service("graphService", ['$q', '$http', function ($q, $http) {
            var apiUri = 'http://dvdlibrary.azurewebsites.net/api/movie/';

            function getMovies(query) {
                var getMoviesRequest = $http({
                    method: 'GET',
                    url: apiUri + 'AvailableMovies?q=' + query
                }).then(function (response) {
                    if (response && response.data) {
                        return response.data;
                    }
                });
                return getMoviesRequest;
            }
            return {
                GetMovies: getMovies,
            };
        }]);
})();