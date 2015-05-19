'use strict';

app.directive('galleryDirective', function () {
    return {
        restrict: 'A',
        templateUrl: window.tattooWeb.angularBaseUrl + 'partials/galleryDirective.html',
        scope: {
            galleryItems:'='
        },
        link: function(scope, element, attrs){
            $('.fancybox').fancybox({
                openEffect: 'none',
                closeEffect: 'none',

                prevEffect: 'none',
                nextEffect: 'none',

                //fitToView: false,

                closeBtn: false,

                helpers: {
                    title: {
                        type: 'inside'
                    },
                    buttons: {}
                },

                afterLoad: function () {
                    this.title = 'Image ' + (this.index + 1) + ' of ' + this.group.length + (this.title ? ' - ' + this.title : '');
                }
            });
        }
    };
});