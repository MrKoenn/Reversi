SPA.Reversi = (function($) {
	const configMap = {};

	function _initModule(wrapper) {
		console.log("Initializing board...");

		wrapper.append("<span class='status self' id='self'></span>");
		wrapper.append("<span class='status turn' id='turn'></span>");
		wrapper.append("<span class='status score' id='score'></span>");
		wrapper.append("<div class='board' id='board'></div>");

		SPA.Data.getBoard().then(board => {
			console.log(board);
			SPA.Model.saveState(board);

			console.log("Board data received, drawing rows...");

			const boardDiv = $("#board");
			for (let i = 0; i < 8; i++) {
				boardDiv.append(`<div class='row' id='row-${i}'></div>`);
			}

			console.log("Rows done, drawing tiles...");

			const tiles = SPA.Model.tiles();
			for (let i = 0; i < tiles.length; i++) {
				const tile = tiles[i];
				const location = tile["location"];
				const id = location["x"] + "," + location["y"];
				$(`#row-${location["x"]}`).append(`<div class='tile' id='tile-${id}'></div>`);
				$(`#tile-${id.replace(",", "\\,")}`).append(`<div class='tile player hidden' id='${id}-player'></div>`);
			}

			console.log("Drawing done, refreshing...");

			SPA.Data.getSelfInfo().then(self => {
				console.log(self);
				$("#self").html(`You are ${self["color"]}`);
				SPA.Model.player = self;
				_refreshBoard();
			});

			boardDiv.click(function(e) {
				_clickTile(e.target);
			});

			setInterval(function() {
					SPA.Data.needsRefresh().then(result => {
						if (result === true) {
							_refreshBoard();
						}
					});
				},
				500);

			console.log("Done!");
		});

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