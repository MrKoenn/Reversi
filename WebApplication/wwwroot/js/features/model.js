SPA.Model = (function ($) {
    const configMap = {};

    let _tiles;
    let _player;

    function _initModule() {
        return true;
    }

    function _saveState(data) {
        _tiles = data["tiles"];
    }

    return {
        initModule: _initModule,
        saveState: _saveState,
        tiles: () => _tiles,
        player: _player
    }
})(jQuery);