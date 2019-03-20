function Widget(message, color) {
    this.message = message;
    this.color = color;
    saveMessage(message, color);

    this.show = function () {
        $(document.documentElement).append(
            `<div id="widget" class="widget-wrapper"><div class="widget ${this.color}">
               <div class="widget-header">
                   ​<button type="button" class="widget-close" onclick="closeWidget()">&times;</button>
               </div>
               <div class="widget-body">
                   <img class="widget-img" src="images/${color}.png" alt="${color}"/>
                   <span class="widget-msg">${this.message}</span>
               </div>
               ​<div class="widget-footer"><button type="button" class="widget-btn" onclick="closeWidget()">Close</button></div>
            </div></div>`
        );
    };
}

function closeWidget() {
    $('#widget').remove();
}

function saveMessage(message, color) {
    let messages;
    if (localStorage.getItem("widget-messages") === null) {
        messages = [];
    } else {
        messages = JSON.parse(localStorage.getItem("widget-messages"));
    }

    if (messages.length >= 10) {
        messages.pop();
    }
    messages.push({
        message: message,
        color: color
    });

    localStorage.setItem("widget-messages", JSON.stringify(messages));
}