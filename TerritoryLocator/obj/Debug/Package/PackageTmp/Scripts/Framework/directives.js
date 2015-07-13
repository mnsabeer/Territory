var TerritoryLocatorDirectives = angular.module("TerritoryLocator.Directives", []);
TerritoryLocatorDirectives.directive("ngConfirmMessage", function () { return { restrict: "A", link: function (e, t, n) { t.bind("click", function () { var t = n.ngConfirmText; bootbox.confirm(t, "No", "Yes", function (t) { if (t) { e.$apply(n.ngConfirmClick) } else { e.$apply(n.ngCancelClick) } }) }) } } });
TerritoryLocatorDirectives.directive("ngMessage", function () { return { restrict: "A", link: function (e, t, n) { t.bind("click", function () { var t = n.ngMessageText; bootbox.alert(t, function (t) { if (t) { e.$apply(n.ngClick) } }) }) } } });
TerritoryLocatorDirectives.directive("ngFileUpload", function (e, t, n) { return { restrict: "A", link: function (t, n, r) { var i = e(r["ngFileSelect"]); n.bind("change", function (e) { var n = [], r, s; r = e.target.files; if (r != null) { for (s = 0; s < r.length; s++) { n.push(r.item(s)) } } i(t, { $files: n, $event: e }) }) } } });
TerritoryLocatorDirectives.directive("ngScrollBar", ["$timeout", function (e) { return { restrict: "A", replace: "false", scope: { contentData: "=" }, link: function (t, n) { n.tinyscrollbar(); t.$watchCollection("contentData", function (t) { n.tinyscrollbar_update("relative"); e(function () { n.tinyscrollbar() }) }, true) } } }])
TerritoryLocatorDirectives.directive("ngModelOnblur", function () { return { restrict: "A", require: "ngModel", priority: 1, link: function (e, t, n, r) { if (n.type === "radio" || n.type === "checkbox") { return } var i = function () { e.$apply(function () { r.$setViewValue(t.val().trim()); r.$render() }) }; t.off("input").off("keydown").off("change").on("focus", function () { e.$apply(function () { r.$setPristine() }) }).on("blur", i).on("keydown", function (e) { if (e.keyCode === 13) { i() } }) } } })

TerritoryLocatorDirectives.directive('ngPrint', function () {

    var printSection = document.getElementById('printSection');
    // if there is no printing section, create one
    if (!printSection) {
        printSection = document.createElement('div');
        printSection.id = 'printSection';
        document.body.appendChild(printSection);
    }

    function link(scope, element, attrs) {
        element.on('click', function () {
            var printContents = document.getElementById(attrs.printElementId).innerHTML;
            var originalContents = document.body.innerHTML;

            if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {
                var popupWin = window.open('', '_blank', 'width=600,height=600,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
                popupWin.window.focus();
                popupWin.document.write('<!DOCTYPE html><html><head>' +
                    '<link rel="stylesheet" type="text/css" href="style.css" />' +
                    '</head><body onload="window.print()"><div class="reward-body">' + printContents + '</div></html>');
            } else {
                var popupWin = window.open('', '_blank', 'width=800,height=600');
                popupWin.document.open();
                popupWin.document.write('<html><head><link rel="stylesheet" type="text/css" href="style.css" /></head><body onload="window.print()">' + printContents + '</html>');
                popupWin.document.close();
            }
            popupWin.document.close();
            return true;
        });

        window.onafterprint = function () {
            printSection.innerHTML = '';
        }
    }

    function printElement(elem) {
        // clones the element you want to print
        var domClone = elem.cloneNode(true);
        printSection.appendChild(domClone);
    }

    return {
        link: link,
        restrict: 'A'
    };
});

TerritoryLocatorDirectives.directive('ngToolTip', function ($timeout) {
    return {
        restrict: 'A',
        replace: 'false',
        link: function (scope, element, attrs) {
            var position = '';
            if (attrs.ngPosition === undefined) {
                position = "top";
            }
            else {
                position = attrs.ngPosition;
            }
            var toolTip = attrs.ngContent;
            element.tooltipster({
                content: $(toolTip),
                contentAsHTML: true,
                trigger: 'hover',
                position: position,
                interactive: true
            });


        }
    };
});

TerritoryLocatorDirectives.directive('ngTextMask', function () {
    return {
        restrict: 'A',
        require: "ngModel",
        link: function (scope, elem, attrs, ctrl, ngModel) {
            var maskText = "(999)999-9999";
            if (!(attrs.ngTextMask == null || attrs.ngTextMask == undefined || attrs.ngTextMask == ""))
                maskText = attrs.ngTextMask;
            var textbox = attrs.$$element;
            textbox.mask(maskText);
        }
    };
});

TerritoryLocatorDirectives.directive('ngSlider', function () {
    return {
        restrict: 'A',
        require: '^ngModel',
        scope: false,
        link: function (scope, elem, attrs, ctrl, ngModel) {
            var maxValue = 100;
            var minValue = 0;
            var disabler = null;
            // Checking for Max value
            if (attrs.max != undefined) {
                maxValue = parseFloat(attrs.max);
            }
            // checking for min value
            if (attrs.min != undefined) {
                minValue = parseFloat(attrs.min);
            }
            // checking the model whether exceeding min and max value range.
            if (scope[attrs.ngModel] < minValue)
                scope[attrs.ngModel] = minValue;
            if (scope[attrs.ngModel] > maxValue)
                scope[attrs.ngModel] = maxValue;
            // Managing the ng-disabled parameter.
            if (attrs.ngDisabled == "")
                attrs.ngDisabled = true;
            else if (attrs.ngDisabled == undefined)
                attrs.ngDisabled = false;
            else if (attrs.ngDisabled.toUpperCase() == "TRUE")
                attrs.ngDisabled = true;
            else {
                disabler = attrs.ngDisabled;
                attrs.ngDisabled = false;
            }

            // Update the slider for outside model changes
            scope.$watch(attrs.ngModel, function (val, oldVal) {
                if (!attrs.ngDisabled && val != undefined) {
                    attrs.$$element.slider("value", val);
                    scope.$parent.this[attrs.ngModel] = val;
                    if (scope.$parent.this[attrs.ngModel] < minValue)
                        scope.$parent.this[attrs.ngModel] = minValue;
                    if (scope.$parent.this[attrs.ngModel] > maxValue)
                        scope.$parent.this[attrs.ngModel] = maxValue;
                }
            });

            elem.slider({
                value: scope[attrs.ngModel],
                max: maxValue,
                min: minValue,
                disabled: attrs.ngDisabled,
                change: function (event, ui) {
                    // Avoiding repeated event triggering
                    if (event.originalEvent != undefined) {
                        if (!attrs.ngDisabled) {
                            scope.$parent.this[attrs.ngModel] = ui.value;
                            // Refreshing the scope.
                            try {
                                scope.$digest();
                            }
                            catch (e) { }
                            if (attrs.ngChange) {
                                try {
                                    scope.$eval(attrs.ngChange);
                                } catch (e) { }
                            }
                        }
                    }
                }
            });
            if (disabler != null) {
                scope.$watch(disabler, function (val) {
                    if (val !== undefined) {
                        if (val === "" || val === true)
                            elem.slider("option", "disabled", true);
                        else {
                            elem.slider("option", "disabled", false);
                        }
                    }
                    else {
                        elem.slider("option", "disabled", false);
                    }

                });
            }

        }
    };
});

TerritoryLocatorDirectives.directive("ngDynamicSrc", function () {
    return {
        link: function (scope, element, attrs) {
            var img, loadImage;
            var parent = scope.$parent;
            img = null;

            loadImage = function (url) {

                img = new Image();
                img.src = url;

                img.onload = function () {
                    element[0].src = url;
                    if (attrs.ngBusyIndicatorId != null && attrs.ngBusyIndicatorId != undefined) {
                        setTimeout(function () {
                            angular.element('#' + attrs.ngBusyIndicatorId).css("display", "none");
                        }, 350);
                    }
                };
            };

            scope.$watch(attrs.ngDynamicSrc, function (newVal, oldVal) {
                if (attrs.ngBusyIndicatorId != null && attrs.ngBusyIndicatorId != undefined)
                    angular.element('#' + attrs.ngBusyIndicatorId).css("display", "block");
                loadImage(newVal);
            });
        }
    };
});
