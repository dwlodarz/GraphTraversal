'use strict';

myApp.controller("MainController", ['$scope', '$log', 'graphService', function ($scope, $log, graphService) {
    $scope.error = "error";
    var getSelectedNodeIds = function () {
        $scope.error = "";

        var selectedNodes = _.filter(alchemy._nodes, function (o) { return o._state === "selected"; });
        if (selectedNodes.length != 2) {
            $scope.error = "No. of selected nodes should be 2."
        }

        return _.map(selectedNodes, 'id');
    }

    var markEdgesAsSelected = function (cocurrentNodeIds) {
        for (var i = 0; i < cocurrentNodeIds.length -1 ; i++) {

            var current = cocurrentNodeIds[i];

        }
        $('g#edge-2-1-0').css('stroke', 'rgb(255, 0, 0)')
        $('g#edge-2-1-0').css('opacity', '1')
    }

    $scope.shortestPathLabel = "Shortest Path";
    $scope.shortestPath = function () {
        var selectedIds = getSelectedNodeIds();
        graphService.GetShortestPath(selectedIds[0], selectedIds[1]).then(function (data) {
            if (data != null && data.ShortestPathResult && data.ShortestPathResult.Path != null && data.ShortestPathResult.Path.length > 0) {
                var concurrentIds = _.map(data.ShortestPathResult.Path, "id");
            }
        });
    }


    var initAlchemyGraphDrawing = function () {
        var config = {
            dataSource: 'http://localhost/GraphTraversal.WebServices/FrontEndService.svc/graphdata',
            linkDistance: function () { return 20; },
            caption: function (node) { return node.caption; },
        };

        alchemy = new Alchemy(config)
    }

    initAlchemyGraphDrawing();

}]);