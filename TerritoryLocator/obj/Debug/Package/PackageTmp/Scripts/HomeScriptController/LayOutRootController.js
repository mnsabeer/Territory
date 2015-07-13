TerritoryLocatorController.controller('LayOutRootController', function ($scope, communicationService) {
 
    init();
    function init() {

        loadPresentationTemplate(HeaderTemplateName, root, function (data) { $scope.$apply(function () { $scope.headerTemplate = data.TemplatePath; }); });
       
     
    };

   
});
