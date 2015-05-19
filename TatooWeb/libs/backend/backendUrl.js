"use strict";

var backendUrl = function () {
    var self = this;
    var baseUrl = window.tattooControlPanel.baseUrl.replace(/\/+$/, '');

    self.artistPanel = {
        searchArtists: baseUrl + '/artistpanel/artists',
        addUpdateDeleteArtist: baseUrl + '/artistpanel/artist',
        updateAvatar: baseUrl + '/artistpanel/artistavatar',
        addArtwork: baseUrl + '/artistpanel/artworks',
        updateDeleteArtwork: baseUrl + '/artistpanel/artwork'
    };

    self.feedbackPanel = {

    };

    self.companyProfilePanel = {
        addUpdateDeleteConnection: baseUrl + '/profilepanel/socialconnection',
        updateCompanyProfile: baseUrl + '/profilepanel/companyprofile'
    };

    return self;
}