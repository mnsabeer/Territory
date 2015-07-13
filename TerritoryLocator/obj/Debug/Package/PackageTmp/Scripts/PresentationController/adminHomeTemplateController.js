sTDBWebNewController.controllerProvider.register("adminHomeTemplateController", function ($scope, $rootScope, popUpService, communicationService) {

    //To store sort state
    var sortState = 1;
    //To store search parameter
    var searchParam = null;

    function paginationInitialization() {

        searchParam = null;

        //To store offset
        $scope.offset = 1;

        sortState = 1;

        //To store total count
        $scope.totalCount = 0;

        //To store limit
        $scope.limit = 20;

        $scope.userlogoffset = 1;
        $scope.userloglimit = 20;
        $scope.userlogtotalCount = 0;

        $scope.SearchResult = false;
        searchName = null;
        $scope.SearchResultCount = 0;

        $scope.paginationMaxSize = 40;
    };


    $scope.Manage = [];
    $scope.isAlertVisible = false;
    $scope.isUserLogVisible = false;
    $scope.AlertList = [];
    $scope.IsSelected = false;
    $scope.userLogIsSearch = false;



    function Minify(value, count) {
        if (value.length > count) {
            value = value.slice(0, count);
            value += ".....";
        }
        return value;
    }


    $scope.isInValid = false;

    init();
    function init() {
        $scope.StartDate = new Date;
        $scope.EndDate = new Date;


        $scope.Manage.value = "Customer";
        $scope.isCustomerVisible = true;
        paginationInitialization();
        fetchCustomerList();
        $rootScope.$on('UpdatedCustomerDataReceivedEvent', function (event, data) {

            onUpdatedCustomerDataReceived(data);
        });

        $rootScope.$on('onEditAlertPopupComplete', function (event, data) {

            onEditAlertPopupComplete(data);
        });

    };

    function fetchCustomerList() {

        var retrievalParams = new Object;
        retrievalParams.SearchParam = searchParam;
        retrievalParams.Limit = $scope.limit;
        retrievalParams.Offset = $scope.offset;
        retrievalParams.SortParam = sortState;
        communicationService.makeWebRequest(homeapiGetCustomers, "POST", retrievalParams, onFetchCustomersComplete, null, true);
    }

    function onFetchCustomersComplete(data) {

        $scope.CustomerList = data.Customers;
        $scope.totalCount = data.Count;
        if (searchParam != null) {
            $scope.SearchResult = true;
            if ((data != null || data != undefined) && data.Count > 0)
                $scope.SearchResultCount = data.Count;
            else
                $scope.SearchResultCount = 0;
        }
    };

    $scope.onSortButtonClicked = function (sortParameter) {

        if (sortParameter != "") {
            var sortParam = parseInt(sortParameter, 10);
            switch (sortParam) {

                case 1:
                    if (sortState == 1) sortState = 2; else sortState = 1;
                    break;
                case 3:
                    if (sortState == 3) sortState = 4; else sortState = 3;
                    break;
                case 5:
                    if (sortState == 5) sortState = 6; else sortState = 5;
                    break;
                case 7:
                    if (sortState == 7) sortState = 8; else sortState = 7;
                    break;
                case 9:
                    if (sortState == 9) sortState = 10; else sortState = 9;
                    break;
                case 11:
                    if (sortState == 11) sortState = 12; else sortState = 11;
                    break;
                case 13:
                    if (sortState == 13) sortState = 14; else sortState = 13;
                    break;
                case 13:
                    if (sortState == 13) sortState = 14; else sortState = 13;
                    break;
                case 15:
                    if (sortState == 15) sortState = 16; else sortState = 15;
                    break;
            }

            fetchCustomerList();

        }

    };


    $scope.onUserLogSortButtonClicked = function (sortParameter) {
       
        if (sortParameter != "") {
            var sortParam = parseInt(sortParameter, 10);
            switch (sortParam) {

                case 1:
                    if (sortState == 1) sortState = 2; else sortState = 1;
                    break;
                default:
                    sortState = 1;
            }
            $scope.showUserLog();
        }
    };
    $scope.onSearchButtonClicked = function (adminHomeTemplateSearchForm, searchVar) {
        $scope.isInValid = false;
        if ((searchVar != "" && searchVar != undefined) && adminHomeTemplateSearchForm.$valid) {
            searchParam = searchVar;
            fetchCustomerList();
        }
        else {
            $scope.isInValid = true;
        }

    };

    $scope.onSearchCloseButtonClicked = function () {
        searchParam = null;
        fetchCustomerList();
        $scope.SearchResult = false;
        $scope.SearchParam = "";
    };

    $scope.pageChanged = function (page) {

        $scope.offset = page;
        fetchCustomerList();
    };

    $scope.userlogpageChanged = function (page) {       
        $scope.userlogoffset = page;
        $scope.showUserLog();
    };


    $scope.showCustomerDetails = function (customerId) {


        var objTemplate = new Object;
        objTemplate.Name = CustomerDetailsPopUpTemplateName;
        objTemplate.Area = root;

        var objData = new Object;
        objData.CustomerId = customerId;

        popUpService.showPopUp(objTemplate, objData);

    };

    $scope.onEditButtonClicked = function (customerId, expDate, isExpired, memberClass, email) {

        var objTemplate = new Object;
        objTemplate.Name = AdminEditCustomerDetailsTemplateName;
        objTemplate.Area = root;
        objTemplate.CustomClass = "smallPopUpWindow";

        var objData = new Object;
        objData.Id = customerId;
        objData.ExpDate = expDate;
        objData.Expired = isExpired;
        objData.MemberClass = memberClass;
        objData.UserId = email;

        popUpService.showPopUp(objTemplate, objData);

    };

    $scope.onResetButtonClicked = function (customerId) {

        var objTemplate = new Object;
        objTemplate.Name = ResetCustomerPasswordTemplateName;
        objTemplate.Area = root;
        objTemplate.CustomClass = "smallPopUpWindow";

        var objData = new Object;
        objData.Id = customerId;

        popUpService.showPopUp(objTemplate, objData);

    };

    $scope.onAddCustomerClicked = function () {

        var objTemplate = new Object;
        objTemplate.Name = AdminAddCustomerTemplateName;
        objTemplate.Area = root;

        popUpService.showPopUp(objTemplate, []);

    };

    function onUpdatedCustomerDataReceived(data) {

        var CustomerInfo = data.UpdatedCustomerInfo;
        var MemberClassName = null;
        angular.forEach(data.MemberClassList, function (value, key) {
            if (value.MemberClass == CustomerInfo.MemberClass) {

                MemberClassName = value.Description;
            }

        });
        angular.forEach($scope.CustomerList, function (value, key) {

            if (value.Id == CustomerInfo.Id) {

                value.UserId = CustomerInfo.UserId;
                value.ExpDate = CustomerInfo.ExpDate;
                value.Expired = CustomerInfo.Expired;
                value.MemberClassDescription = MemberClassName;
                value.MemberClass = CustomerInfo.MemberClass;
            }

        });

        popUpService.hidePopUp();
    };


    $scope.manageCustomer = function () {

        $scope.Manage.value == 'Customer';
        $scope.isCustomerVisible = true;
        $scope.isUserLogVisible = false;
        $scope.isAlertVisible = false;
        fetchCustomerList();

    };


    $scope.manageAlerts = function () {

        $scope.isCustomerVisible = false;
        $scope.isAlertVisible = true;
        $scope.isUserLogVisible = false;
        fetchAlertList();
        paginationInitialization();
    };

    $scope.searchNameClicked = function (searchParam) {
       

        searchName = searchParam;
        $scope.userLogIsSearch = true;
        $scope.showUserLog();
    };

    $scope.onClearSearchClicked = function () {
       

        searchName = null;
        $scope.userLogIsSearch = false;
        $scope.showUserLog();
    };

    $scope.showUserLog = function () {
        

        $scope.isCustomerVisible = false;
        $scope.isAlertVisible = false;
        $scope.isUserLogVisible = true;
        fetchUserLogList();

    };

    $scope.showUserLogOnButtonClick = function (adminHomeTemplateUserLogForm, StartDate, EndDate) {
      
        if (adminHomeTemplateUserLogForm.$valid) {
            if (StartDate != undefined && EndDate != undefined) {
                $scope.StartDate = formatDate(new Date(StartDate));
                $scope.EndDate = formatDate(new Date(EndDate));
            }

            $scope.isCustomerVisible = false;
            $scope.isAlertVisible = false;
            $scope.isUserLogVisible = true;
            fetchUserLogList();
        }
    };


    function fetchUserLogList() {
        if ($scope.EndDate >= $scope.StartDate) {
            var userLogFilterModel = new Object;
            userLogFilterModel.StartDate = formatDate(new Date($scope.StartDate));
            userLogFilterModel.EndDate = formatDate(new Date($scope.EndDate));
            userLogFilterModel.Limit=$scope.userloglimit;
            userLogFilterModel.Offset=$scope.userlogoffset;
            userLogFilterModel.SortParam = sortState;
            userLogFilterModel.SearchParam = searchName;
            
            communicationService.makeWebRequest(homeapiGetUserLog, "POST", userLogFilterModel, onFetchUserLogComplete, null, true);
        }
        else {
            showMessage('End date should be greater than start date.');
        }
    };

    function onFetchUserLogComplete(data) {
      
        $scope.UserLogList = [];
      
        if (data.logData != null && data.logData != undefined)
            if (data.logData.length > 0) {
                $scope.userlogtotalCount = data.countData[0].fnuserlogcount;
                angular.forEach(data.logData, function (value) {

                    $scope.UserLogList.push({ Id: value.id, AccessedTime: value.accessedtime.toString().replace(/T/g, " "), CustomerId: value.customerid, Name: value.fullname, Deatils: value.searchedaddress });
                });
            }
        $scope.StartDate = new Date($scope.StartDate.toString());
        $scope.EndDate = new Date($scope.EndDate.toString());
    };
    $scope.onViewUserLogClicked = function (id) {

        var objTemplate = new Object;
        objTemplate.Name = UserLogDetailsPopupTemplateName;
        objTemplate.Area = root;

        var objData = new Object;
        objData.Id = id;

        popUpService.showPopUp(objTemplate, objData);
    };

    function fetchAlertList() {

        var retrievalParams = new Object;
        retrievalParams.SearchParam = searchParam;
        retrievalParams.Limit = $scope.limit;
        retrievalParams.Offset = $scope.offset;
        retrievalParams.SortParam = sortState;
        communicationService.makeWebRequest(homeapiGetAlerts, "POST", retrievalParams, onFetchAlertsComplete, null, true);
    };

    function onFetchAlertsComplete(data) {

        $scope.AlertList = [];
        if (data != null && data != undefined)
            if (data.Alerts.length > 0) {
                angular.forEach(data.Alerts, function (value) {
                    var isPublished = (value.IsPublished) ? "Yes" : "No";

                    $scope.AlertList.push({ Id: value.ID, Header: value.Header, AlertText: value.AlertText, AlertMinText: Minify(value.AlertText, 50), IsPublished: isPublished, IsSelected: value.IsPublished })
                });
            }
    };

    function onDeleteAlertComplete(data) {

        if (data.ID != undefined) {
            angular.forEach($scope.AlertList, function (value, key) {

                if (value.Id == data.ID) {

                    $scope.AlertList.splice(key, 1);
                }

            });
        }
        else {
            showMessage(data);
        }

    };
    function onEditAlertPopupComplete(data) {
        if (data.newAlert != null && data.newAlert != undefined) {

            var newAlerts = [];
            data.newAlert.AlertMinText = Minify(data.newAlert.AlertText, 50);
            data.newAlert.IsPublished = (data.newAlert.IsPublished) ? "Yes" : "No";


            angular.forEach($scope.AlertList, function (value, key) {

                if (value.Id == data.newAlert.Id) {
                    value.Header = data.newAlert.Header;
                    value.IsPublished = data.newAlert.IsPublished;
                    value.AlertMinText = data.newAlert.AlertMinText;
                    value.AlertText = data.newAlert.AlertText;
                }

            });

        }
    };

    function onPublishAlertCompleted(data) {

        if (data != null) {
            showMessage("Successfully Published");
            angular.forEach($scope.AlertList, function (value, key) {

                if (value.IsSelected) {
                    value.IsPublished = "Yes";
                    value.IsSelected = true;
                }

            });
        }

    };

    function onUnPublishAlertCompleted(data) {

        if (data != null) {
            showMessage("Successfully UnPublished");
            angular.forEach($scope.AlertList, function (value, key) {

                if (value.IsSelected) {
                    value.IsPublished = "No";
                    value.IsSelected = false;
                }

            });
        }

    };
    $scope.onAlertCheckedForUnPublishing = function () {

        var tempList = [];

        angular.forEach($scope.AlertList, function (value, key) {

            if (value.IsSelected) {
                tempList.push(value);
            }

        });
        if (tempList != null && tempList.length > 0)
            communicationService.makeWebRequest(homeapiToUnPublishAlerts, "POST", tempList, onUnPublishAlertCompleted, null, true);
        else
            showMessage("Please Select atleast one item");
    };


    $scope.onAlertCheckedForPublishing = function () {

        var tempList = [];

        angular.forEach($scope.AlertList, function (value, key) {

            if (value.IsSelected) {
                tempList.push(value);
            }

        });
        if (tempList != null && tempList.length > 0)
            communicationService.makeWebRequest(homeapiToPublishAlerts, "POST", tempList, onPublishAlertCompleted, null, true);
        else
            showMessage("Please Select atleast one item");
    };

    $scope.onEditAlertButtonClicked = function (data) {

        var Alert = new Object;
        angular.forEach($scope.AlertList, function (value, key) {

            if (value.Id == data) {

                Alert = angular.copy(value);
            }

        });
        var objTemplate = new Object;
        objTemplate.Name = AdminAddAlertTemplateName;
        objTemplate.Area = root;
        objTemplate.CustomClass = "smallPopUpWindow";

        Alert.ID = data;
        popUpService.showPopUp(objTemplate, Alert);
    };
    $scope.onDeleteAlertButtonClicked = function (data) {

        communicationService.makeWebRequest(homeapiDeleteAlert, "POST", data, onDeleteAlertComplete, null, true);
    };

    $scope.onAddAlertClicked = function () {
        var objTemplate = new Object;
        objTemplate.Name = AdminAddAlertTemplateName;
        objTemplate.Area = root;
        objTemplate.CustomClass = "smallPopUpWindow";

        popUpService.showPopUp(objTemplate, []);
    };

    $rootScope.$on('onClosePopup', function (event, data) {
        $scope.totalCount++;
        var customers = $scope.CustomerList;
        customers.splice(customers.length - 1, 1);
        var newCustomer = new Object;
        newCustomer.Id = data.BasicDetails.CustomerId;
        newCustomer.UserId = data.BasicDetails.Email;
        newCustomer.FirstName = data.BasicDetails.FirstName;
        newCustomer.LastName = data.BasicDetails.LastName;
        newCustomer.MemberClass = data.BasicDetails.MemberClass;
        newCustomer.ExpDate = data.BasicDetails.ExpiryDate;
        newCustomer.Expired = data.BasicDetails.IsExpired;
        newCustomer.Lastlogindate = data.BasicDetails.LastLogin;
        newCustomer.MemberClassDescription = data.BasicDetails.MemberClassDescription;
        var customerList = [];
        customerList.push(newCustomer);
        for (i = 0; i < customers.length; i++) {
            customerList.push(customers[i]);
        }
        $scope.CustomerList = customerList;
    });

    $rootScope.$on('onCloseAddAlertPopup', function (event, data) {
        if (data.newAlert != null && data.newAlert != undefined) {

            var newAlerts = [];
            data.newAlert.AlertMinText = Minify(data.newAlert.AlertText, 50);
            data.newAlert.IsPublished = (data.newAlert.IsPublished) ? "Yes" : "No";
            newAlerts.push(data.newAlert);
            angular.forEach($scope.AlertList, function (value) {

                newAlerts.push(value);
            });
            $scope.AlertList = newAlerts;
        }
    });
    function formatDate(date) {
       
        var hours = date.getHours();
        var minutes = date.getMinutes();
        var ampm = hours >= 12 ? 'pm' : 'am';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;
        var strTime = hours + ':' + minutes + ' ' + ampm;
        return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear() + "  " + strTime;
    };

    // Date picker


    // Disable weekend selection
    $scope.disabled = function (date, mode) {
        return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
    };

    $scope.toggleMin = function () {
        $scope.minDate = $scope.minDate ? null : new Date();
    };
    $scope.toggleMin();

    $scope.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened = true;
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1
    };

    function tempformatDate(date) {
        var d = new Date(date);
        var hh = d.getHours();
        var m = d.getMinutes();
        var s = d.getSeconds();
        var dd = "AM";
        var h = hh;
        if (h >= 12) {
            h = hh - 12;
            dd = "PM";
        }
        if (h == 0) {
            h = 12;
        }
        m = m < 10 ? "0" + m : m;

        s = s < 10 ? "0" + s : s;

        /* if you want 2 digit hours:
        h = h<10?"0"+h:h; */

        var pattern = new RegExp("0?" + hh + ":" + m + ":" + s);

        var replacement = h + ":" + m;
        /* if you want to add seconds
        replacement += ":"+s;  */
        replacement += " " + dd;

        return date.replace(pattern, replacement);
    };

    function getFormattedDate(input) {
       
        var pattern = /(.*?)\/(.*?)\/(.*?)$/;
        var result = input.replace(pattern, function (match, p1, p2, p3) {
            var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
            return (p2 < 10 ? "0" + p2 : p2) + " " + months[(p1 - 1)] + " " + p3;
        });
        return(result);
    };
    function getDate(date) {

        return date.getMonth() + 1 + "-" + date.getDate() + "-" + date.getFullYear();
    };

});