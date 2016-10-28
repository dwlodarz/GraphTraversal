'use strict';

myApp.controller("MainController", ['$scope', '$log', 'graphService', function ($scope, $log, movieService) {
    $scope.shortestPathLabel = "Shortest Path";
    $scope.shortestPath = function ()
    {
        alert('test')
    }


    var initAlchemyGraphDrawing = function ()
    {
        var config = {
            dataSource: 'http://localhost/GraphTraversal.WebServices/FrontEndService.svc/graphdata',

            linkDistance: function () { return 40; },

            nodeTypes: { "node_type": ["Maintainer", "Contributor"] },
            caption: function (node) { return node.caption; }
        };

        alchemy = new Alchemy(config)
    }

    initAlchemyGraphDrawing();

}]);