console.log("Data loaded");
SPA.Data = (function ($) {
    const configMap = {
        environment: "development",
        endpoints: {
            development: "https://localhost:44314/api/game",
            production: "api/game"
        },
        boardPath: "/board",
        movePath: "/move",
        selfPath: "/self",
        turnPath: "/turn",
        refreshPath: "/refresh"
    };

    function _initModule(environment) {
        configMap.environment = environment;
        return true;
    }

    function _getBoard() {
        let path = configMap.endpoints[configMap.environment] + configMap.boardPath;
        return _simpleGet(path);
    }

    function _makeMove(x, y) {
        let path = configMap.endpoints[configMap.environment] + configMap.movePath;
        let data = {
            X: x,
            Y: y
        };
        let promise = new Promise((resolve, reject) => {
            $.post(path, data, function (result) {
                resolve(result);
            }).fail(function () {
                reject("Request failed");
            });
        });
        return Promise.resolve(promise);
    }

    function _getSelfInfo() {
        let path = configMap.endpoints[configMap.environment] + configMap.selfPath;
        return _simpleGet(path);
    }

    function _getTurn() {
        let path = configMap.endpoints[configMap.environment] + configMap.turnPath;
        return _simpleGet(path);
    }

    function _needsRefresh() {
        let path = configMap.endpoints[configMap.environment] + configMap.refreshPath;
        return _simpleGet(path);
    }

    function _simpleGet(path) {
        let promise = new Promise((resolve, reject) => {
            $.get(path, function (data) {
                resolve(data);
            }).fail(function () {
                reject("Request failed");
            });
        });
        return Promise.resolve(promise);
    }

    return {
        initModule: _initModule,
        getBoard: _getBoard,
        makeMove: _makeMove,
        getSelfInfo: _getSelfInfo,
        getTurn: _getTurn,
        needsRefresh: _needsRefresh
    }
})(jQuery);