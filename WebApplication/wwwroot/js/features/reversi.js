SPA.Reversi = (function ($) {
    const configMap = {};

    function _initModule(wrapper) {
        console.log("Initializing board...");

        SPA.Data.getBoard().then(board => {
            console.log(board);
            SPA.Model.saveState(board);

            const rows = [];

            for (let i = 0; i < 8; i++) {
                rows[i] = {id: i, tiles: []};
            }

            const tiles = SPA.Model.tiles();
            for (let i = 0; i < tiles.length; i++) {
                const location = tiles[i]["location"];
                rows[location["x"]].tiles.push({id: `${location["x"]},${location["y"]}`});
            }

            let result = spa_templates.templates.reversi.gameplay({rows: rows});
            result = result.replace(/(?:\r\n|\r|\n| {2})/g, '');
            wrapper.append(result);

            SPA.Data.getSelfInfo().then(self => {
                console.log(self);
                $("#self").html(`You are ${self["color"]}`);
                SPA.Model.player = self;
                _refreshBoard();
            });

            $("#board").click(function (e) {
                _clickTile(e.target);
            });

            setInterval(function () {
                    SPA.Data.needsRefresh().then(result => {
                        if (result === true) {
                            _refreshBoard();
                        }
                    });
                },
                500
            );

            console.log("Done!");
        });

        let test = new Widget("Welcome to Reversi! To see the rules, please click <a href='http://www.flyordie.com/games/help/reversi/en/games_rules_reversi.html' target='_blank'>here</a>. Play fair and have fun!", "green");
        test.show();

        return true;
    }

    function _refreshBoard() {
        SPA.Data.getBoard().then(board => {
            console.log(`Received new board: ${board}`);
            const tiles = SPA.Model.tiles();
            for (let i = 0; i < tiles.length; i++) {
                const newTile = board["tiles"][i];
                const location = newTile["location"];
                const id = location["x"] + "," + location["y"];
                $(`#${id.replace(",", "\\,")}-player`).attr("class", `tile player ${newTile["color"]}`);
            }

            SPA.Model.saveState(board);
        });


        SPA.Data.getTurn().then(turn => {
            $("#turn").html(`It is ${turn["color"]}'s turn`);
            if (SPA.Model.player["color"] === turn["color"]) {
                SPA.Data.getMoves().then(moves => {
                    for (let i = 0; i < moves.length; i++) {
                        const tile = moves[i];
                        const location = tile["location"];
                        const id = location["x"] + "," + location["y"];
                        $(`#tile-${id.replace(",", "\\,")}`).attr("class", "tile ghost");
                    }
                });
            }
        });
        SPA.Data.getScore().then(score => {
            $("#score").html(`White: ${score[0]}, Black: ${score[1]}`);
        });
    }

    function _clickTile(tile) {
        const raw = tile.id.replace("tile-", "").split(",");
        for (let i = 0; i < SPA.Model.tiles().length; i++) {
            const t = SPA.Model.tiles()[i];
            const location = t["location"];
            const id = location["x"] + "," + location["y"];
            $(`#tile-${id.replace(",", "\\,")}`).removeClass("ghost");
        }

        SPA.Data.makeMove(parseInt(raw[0]), parseInt(raw[1]));
    }

    return {
        initModule: _initModule
    }
})(jQuery);