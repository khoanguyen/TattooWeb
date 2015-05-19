"use strict";

app.controller('MainPageController', function ($scope, $http, appUrl, $location) {
    init();

    function init() {
        $scope.isLoading = true;
        $http({ method: 'GET', url: appUrl.service(urlConfig.getCompanyProfileUrl) })
             .success(function (data) {
                 $scope.isLoading = false;
                 $scope.introTitle = data.title;
                 $scope.intro = data.intro;
                 $scope.homeImage = data.mainImage;                 
             })
             .error(function () {
                 $location.path(nav.errorNav.link);
             });
    }
});

app.controller('GalleryPageController', function ($scope, $http, $routeParams, appUrl) {
    init();

    $scope.showCarousel = function () {
        $scope.isShowed = !$scope.isShowed;
        $scope.showHideButton = $scope.isShowed ? '../../Content/images/site_images/button-tray-up.png' :
                                                  '../../Content/images/site_images/button-tray-down.png';
    }

    $scope.onClickCallBack = function (id) {
        $scope.isReload = true;
        getData(id);
    }

    function init() {
        $scope.isShowed = true;
        $scope.isLoading = true;
        $scope.isReload = false;
        $scope.showHideButton = '../../Content/images/site_images/button-tray-down.png';
        getData(null);
        getCarouselData();
    }

    function getData(id) {
        id = (id) ? id : $routeParams.key;
        var getUrl = (!id) ? appUrl.service(urlConfig.getArtistGalleryUrl) : appUrl.service(urlConfig.getArtistGalleryUrl) + "/" + id;
        $http({ method: 'GET', url: getUrl })
                .success(function (data) {
                    $scope.artistName = data.artistName,
                    $scope.contactNumber = data.contactNumber,
                    $scope.artistImage = data.artistImage,
                    $scope.works = data.works;
                    $scope.isLoading = false;
                    $scope.isReload = false;
                    //$scope.apply();
                }).error(function () {

                });
    }

    function getCarouselData() {
        $http({ method: 'GET', url: appUrl.service(urlConfig.getAllArtistUrl) })
                .success(function (data) {
                    $scope.carousel = data;
                }).error(function () {
                    $scope.carousel = [];
                });
    }

});

app.controller('AboutPageController', function ($scope) {
    init();

    function init() {

    }
});

app.controller('ContactPageController', function ($scope, $http, appUrl) {
    init();

    $scope.onReset = function (form) {
        $scope.showResult = false;
        form.$setPristine();
    };

    $scope.sendContact = function (form) {
        $scope.isSending = true;
        $http.post(appUrl.service(urlConfig.postContactUrl),
                    {
                        customerName: $scope.contact.yourName,
                        customerEmail: $scope.contact.yourEmail,
                        customerPhone: $scope.contact.yourPhone,
                        message: $scope.contact.yourMessage
                    })
                    .success(function () {
                        $scope.isSending = false;
                        $scope.showResult = true;
                        $scope.isError = false;
                        $scope.sentMessage = 'Your message has been sent. Thank you for your comment.';
                        form.$setPristine();
                    })
                    .error(function () {
                        $scope.isSending = false;
                        $scope.showResult = true;
                        $scope.isError = true;
                        $scope.sentMessage = 'Your message can not be sent at the moment. Please try again or contact us through phone.' +
                                             'We are sorry for this inconvenience.';
                    });
    };

    function init() {
        $scope.isLoading = true;
        getProfile();
        $scope.contact = {
            yourName: '',
            yourEmail: '',
            yourPhone: '',
            yourMessage: ''
        };
        $scope.isSending = false;
        $scope.showResult = false;
        $scope.isError = false;
        $scope.emailPattern = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
        $scope.phonePattern = /^[0-9\-]{1,20}/;
    }

    function getProfile() {
        $http.get(appUrl.service(urlConfig.getCompanyProfileUrl))
             .success(function (response) {
                 $scope.profile = {
                     name: 'new generation tattoo',
                     phoneNo: response.phone,
                     address: response.address
                 };
                 $scope.isLoading = false;
             })
             .error(function () {
                 $scope.profile = {
                     name: 'new generation tattoo',
                     phoneNo: '#####',
                     address: '#####'
                 };
                 $scope.isLoading = false;
             });
    }
});

app.controller('ErrorPageController', function ($scope) {

})