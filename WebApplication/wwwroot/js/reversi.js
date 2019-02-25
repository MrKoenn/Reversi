SPA.Reversi = (function ($) {
    const configMap = {};

    function _initModule(wrapper) {
        console.log("Initializing board...");

        wrapper.append("<span class='status self' id='self'></span>");
        wrapper.append("<span class='status turn' id='turn'></span>");
        wrapper.append("<div class='board' id='board'></div>");

        SPA.Data.getBoard().then(board => {
            SPA.Model.saveState(board);

            console.log("Board data received, drawing rows...");

            let boardDiv = $("#board");
            for (let i = 0; i < 8; i++) {
                boardDiv.append("<div class='row' id='row-" + i + "'></div>");
            }

            console.log("Rows done, drawing tiles...");

            let tiles = SPA.Model.tiles();
            for (let i = 0; i < tiles.length; i++) {
                let tile = tiles[i];
                let location = tile["location"];
                let id = location["x"] + "," + location["y"];
                $("#row-" + location["x"]).append("<div class='tile' id='tile-" + id + "'></div>");
                $("#tile-" + id.replace(",", "\\,")).append("<div class='tile player hidden' id='" + id + "-player'></div>");
            }

            console.log("Drawing done, refreshing...");

            _refreshBoard(true);

            SPA.Data.getSelfInfo().then(self => {
                console.log(self);
                $("#self").html("You are " + self["name"]);
            });

            boardDiv.click(function (e) {
                _clickTile(e.target);
            });
            
            setInterval(function () {
                SPA.Data.needsRefresh().then(result => {
                    if (result === true) {
                        _refreshBoard();
                    }
                })
            }, 500);

            console.log("Done!");
        });

        return true;
    }

    function _refreshBoard(first = false) {
        SPA.Data.getBoard().then(board => {
            let tiles = SPA.Model.tiles();
            for (let i = 0; i < tiles.length; i++) {
                let oldTile = tiles[i];
                let newTile = board["tiles"][i];
                let location = newTile["location"];
                if (first || oldTile["ownerId"] !== newTile["ownerId"]) {
                    let id = location["x"] + "," + location["y"];
                    let color = newTile["ownerId"] === 0 ? "white" : newTile["ownerId"] === -1 ? "hidden" : "black";
                    console.log("Tile " + id + " changed, updating...");
                    $("#" + id.replace(",", "\\,") + "-player").attr("class", "tile player " + color);
                }
            }

            SPA.Model.saveState(board);
        });

        SPA.Data.getTurn().then(turn => {
            $("#turn").html("It is " + turn["name"] + "'s turn");
        });
    }

    function _clickTile(tile) {
        let raw = tile.id.replace("tile-", "").split(",");
        SPA.Data.makeMove(parseInt(raw[0]), parseInt(raw[1])).then(value => {
            if (value === true) {
                _refreshBoard();
            }
        });
    }

    return {
        initModule: _initModule
    }
})(jQuery);