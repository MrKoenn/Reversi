SPA.Data = (function ($) {
    const configMap = {
        environment: "development",
        endpoints: {
            development: "https://localhost:44314/api/game",
            production: "https://localhost:44314/api/game"
        },
        boardPath: "/board",
        movePath: "/move",
        selfPath: "/self",
        turnPath: "/turn",
        refreshPath: "/refresh",
        scorePath: "/score",
        movesPath: "/moves"
    };

    function _initModule(environment) {
        configMap.environment = environment;
        return true;
    }

    function _getBoard() {
        const path = configMap.endpoints[configMap.environment] + configMap.boardPath;
        return _simpleGet(path);
    }

    function _makeMove(x, y) {
        const path = configMap.endpoints[configMap.environment] + configMap.movePath;
        const data = {
            X: x,
            Y: y
        };
        const promise = new Promise((resolve, reject) => {
            $.post(path, data, function (result) {
                resolve(result);
            }).fail(function () {
                reject("Request failed");
            });
        });
        return Promise.resolve(promise);
    }

    function _getSelfInfo() {
        const path = configMap.endpoints[configMap.environment] + configMap.selfPath;
        return _simpleGet(path);
    }

    function _getTurn() {
        const path = configMap.endpoints[configMap.environment] + configMap.turnPath;
        return _simpleGet(path);
    }

    function _needsRefresh() {
        const path = configMap.endpoints[configMap.environment] + configMap.refreshPath;
        return _simpleGet(path);
    }

    function _getScore() {
        const path = configMap.endpoints[configMap.environment] + configMap.scorePath;
        return _simpleGet(path);
    }

    function _getMoves() {
        const path = configMap.endpoints[configMap.environment] + configMap.movesPath;
        return _simpleGet(path);
    }

    function _simpleGet(path) {
        const promise = new Promise((resolve, reject) => {
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
        getScore: _getScore,
        needsRefresh: _needsRefresh,
        getMoves: _getMoves
    }
})(jQuery);