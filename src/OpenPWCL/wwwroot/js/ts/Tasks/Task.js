"use strict";
var Task = /** @class */ (function () {
    function Task(func) {
        this.status = "notDeffined";
        if (func !== undefined) {
            this.func = func;
            this.status = "notStarted";
            try {
                this.onDefined(this);
                this.onStateChange(this);
            }
            catch (_a) {
            }
        }
    }
    Task.prototype.run = function (input) {
        this.status = "running";
        try {
            this.onStateChange(this);
            this.onStarted(this);
        }
        catch (_a) { }
        try {
            var val = this.func(input);
            this.status = "finished";
            try {
                this.onStateChange(this);
                this.onFinished(this);
            }
            catch (_b) { }
            return val;
        }
        catch (_c) {
            this.status = "thrown";
            try {
                this.onStateChange(this);
                this.onError(this);
            }
            catch (_d) { }
        }
        return undefined;
    };
    return Task;
}());
//# sourceMappingURL=Task.js.map