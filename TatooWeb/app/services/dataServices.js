"use strict";

app.service("dataServices", [function ($http) {
    var self = this;

    // OMG .... !!! (~~")
    // This is the reason why you should not repeat global code at private scope
    //var serviceBaseUrl = window.location.protocol + "//" + window.location.host + "home/";

    var serviceBaseUrl = window.tattoWeb.baseUrl + "home/";

    self.getCompanyProfile = function (successCallback, errorCallback)
    {
        $http({ method: 'GET', url: serviceBaseUrl + 'GetCompanyProfile' })
             .success(successCallback)
             .error(errorCallback);
    };
}]);