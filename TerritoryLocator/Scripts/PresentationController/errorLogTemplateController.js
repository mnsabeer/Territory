sTDBWebNewController.controllerProvider.register('errorLogTemplateController', function ($scope, $rootScope, communicationService, popUpService) {
   
    searchParam = null;
    //To store offset
    $scope.offset = 1;
    sortState = 1;
    //To store total count
    $scope.totalCount = 0;
    //To store limit
    $scope.limit = 50;

    init();

    function init() {
        fetchErrorList();
    }
    function fetchErrorList() {
        
        var retrievalParams = new Object;
        retrievalParams.SearchParam = searchParam;
        retrievalParams.Limit = $scope.limit;
        retrievalParams.Offset = $scope.offset;
        retrievalParams.SortParam = sortState;
        communicationService.makeWebRequest(homeapiFetchErrorLog, "POST", retrievalParams, onFetchErrorLogComplete, null, true);
    }
    function onFetchErrorLogComplete(data)
    {
       
        $scope.ErrorList = data.ErrorList;
        $scope.totalCount = data.Count;

    }
    $scope.pageChanged = function (page) {

        $scope.offset = page;
        fetchErrorList();
    };
    $scope.onClickViewError = function (errorId) {
        communicationService.makeWebRequest(homeapiFetchErrorDetailsById + errorId, "GET", {}, onFetchErrorDetailsByIdComplete, null, true);
    };
    function onFetchErrorDetailsByIdComplete(data) {
        if (data != null) {
            var objTemplate = new Object;
            objTemplate.Name = ViewErrorDetailsPopUpTemplateName;
            objTemplate.Area = root;
            objTemplate.CustomClass = "viewerrorPopUpWindow";
            var objData = new Object;
            objData.Error = data;

            popUpService.showPopUp(objTemplate, objData);

        }
       
    }
});