SPA.Model = (function ($) {
    const configMap = {};

    let _tiles;

    function _initModule() {
        return true;
    }

    function _saveState(data) {
        _tiles = data["tiles"];
    }

    return {
        initModule: _initModule,
        saveState: _saveState,
        tiles: () => _tiles
    }
})(jQuery);