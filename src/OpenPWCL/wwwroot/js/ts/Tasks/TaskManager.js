"use strict";
//import * as $ from "jquery";
//import { Task } from "./Task";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [0, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
var TaskManager = /** @class */ (function () {
    function TaskManager(rootElement) {
        this.root = $(rootElement).find("[data-task-id]").get(0);
        this.init();
    }
    TaskManager.prototype.init = function () {
        var _this = this;
        this.taskId = this.unStrToStr(this.root.dataset.taskId, "taskId");
        this.returnUri = this.unStrToStr(this.root.dataset.taskReturnUri, "returnUri");
        this.requestUri = this.unStrToStr(this.root.dataset.taskUri, "taskUri");
        this.taskName = this.unStrToStr(this.root.dataset.taskName, "taskName");
        this.task = new Task(function (a) { return "Not inited"; });
        this.task.onStateChange = function (task) {
            var newElement = document.createElement("p");
            newElement.innerHTML = _this.taskName + ": " + task.status;
            _this.root.appendChild(newElement);
        };
    };
    TaskManager.prototype.unStrToStr = function (input, paramName) {
        if (input === undefined) {
            console.log(paramName + " undefined");
            console.log(this);
            return "undefined";
        }
        return input;
    };
    TaskManager.prototype.StartWorking = function () {
        return __awaiter(this, void 0, void 0, function () {
            var a, _a, _b;
            return __generator(this, function (_c) {
                switch (_c.label) {
                    case 0:
                        if (!true) return [3 /*break*/, 9];
                        _c.label = 1;
                    case 1:
                        _c.trys.push([1, 7, , 8]);
                        return [4 /*yield*/, $.ajax({
                                accepts: { application: "application/json" },
                                method: "GET",
                                url: "/tasks/GetTaskToSolve",
                                async: true,
                                contentType: "application/json"
                            })];
                    case 2:
                        a = _c.sent();
                        this.task = new Task(eval(a.javascriptCode));
                        this.taskId = a.id;
                        this.taskName = a.name;
                        _c.label = 3;
                    case 3:
                        _c.trys.push([3, 5, , 6]);
                        return [4 /*yield*/, this.RunTests()];
                    case 4:
                        _c.sent();
                        return [3 /*break*/, 6];
                    case 5:
                        _a = _c.sent();
                        return [3 /*break*/, 6];
                    case 6: return [3 /*break*/, 8];
                    case 7:
                        _b = _c.sent();
                        return [3 /*break*/, 8];
                    case 8: return [3 /*break*/, 0];
                    case 9: return [2 /*return*/];
                }
            });
        });
    };
    TaskManager.prototype.RunTests = function () {
        return __awaiter(this, void 0, void 0, function () {
            var a, curId, inputParams, res;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, $.ajax({
                            accepts: { application: "application/json" },
                            method: "GET",
                            url: "/TaskInstances/GetTaskInstance/?taskId=" + this.taskId,
                            async: true,
                            contentType: "application/json"
                        })];
                    case 1:
                        a = _a.sent();
                        _a.label = 2;
                    case 2:
                        if (!true) return [3 /*break*/, 4];
                        curId = a.id;
                        inputParams = JSON.parse(a.inputParams);
                        res = encodeURI(JSON.stringify(this.task.run(inputParams)));
                        return [4 /*yield*/, $.ajax({
                                accepts: { application: "application/json" },
                                method: "POST",
                                url: "/TaskInstances/TaskInstanceSolved/" + curId + "/?result=" + res,
                                data: JSON.stringify({
                                    result: res
                                }),
                                async: true,
                                contentType: "application/json"
                            })];
                    case 3:
                        a = _a.sent();
                        return [3 /*break*/, 2];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    return TaskManager;
}());
//# sourceMappingURL=TaskManager.js.map