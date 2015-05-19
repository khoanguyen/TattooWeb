'use strict';

app.controller('NavController', function ($scope, $location) {
    $scope.menuItems = nav.menuItems;

    handelMenuOnRefreshOrLoad();

    $scope.getClass = function (menuId) {
        setClass(menuId);
    };

    $scope.optionNav = function (menuId, link) {
        setClass(menuId);
        $location.path(link);
    }

    function handelMenuOnRefreshOrLoad() {
        var currentPath = $location.path().toLowerCase();
        if (currentPath === nav.defaultNav) return;
        for (var i = 0; i < $scope.menuItems.length; i++) {
            $scope.menuItems[i].current = false;
            if (i > 0 && currentPath.substring(0, $scope.menuItems[i].link.length) === $scope.menuItems[i].link) {
                $scope.menuItems[i].current = true;
                break;
            }
        }
    }

    function setClass(menuId) {
        for (var i = 0; i < $scope.menuItems.length; i++) {
            $scope.menuItems[i].current = false;
            if ($scope.menuItems[i].id === menuId) {
                $scope.menuItems[i].current = true;
            }
        }
    }

});

app.controller('SocialNetworkController', function ($scope, $http) {
    init();

    $scope.changeImage = function (network) {
        network.isHover = !network.isHover;
        network.image = network.isHover ? network.hoverImage : network.mainImage;
    }

    function init() {
        $http({ method: 'GET', url: '/home/getsocialnetworks' })
                .success(function (data) {
                    refineData(data);
                    $scope.socialNetworks = data;
                }).error(function () { $scope.socialNetworks = []; });
    }

    function refineData(data) {
        for (var i = 0; i < data.length; i++) {
            data[i].image = data[i].mainImage;
            data[i].isHover = false;
        }
    }
});