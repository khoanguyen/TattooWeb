"use strict";
 
var tattooNav = function() {        
    return {
        menuItems: [{ id: 1, menuTitle: 'Main', link: '/', controller: 'MainPageController', templateUrl: 'mainPage.html', current: true },
                { id: 2, menuTitle: 'Gallery', link: '/gallery', controller: 'GalleryPageController', templateUrl: 'galleryPage.html', current: false },
                { id: 3, menuTitle: 'About Us', link: '/about', controller: 'AboutPageController', templateUrl: 'aboutPage.html', current: false },
                { id: 4, menuTitle: 'Contacts', link: '/contact', controller: 'ContactPageController', templateUrl: 'contactPage.html', current: false }],
        subNav: [{ link: '/gallery/:key/:name', controller: 'GalleryPageController', templateUrl: 'galleryPage.html'} ],
        errorNav: { link: '/error', controller: 'ErrorPageController', templateUrl: 'errorPage.html'},
        defaultNav: '/'
    };
};

