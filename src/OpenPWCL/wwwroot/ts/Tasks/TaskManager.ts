//import * as $ from "jquery";
//import { Task } from "./Task";

class TaskManager {
    public task: Task;
    public taskId: string;
    public returnUri: string;
    public requestUri: string;
    public taskName: string;
    public root: HTMLElement;
    public constructor(rootElement: HTMLElement) {
        this.root = $(rootElement).find("[data-task-id]").get(0);
        this.init();
    }
    public init() {
        this.taskId = this.unStrToStr(this.root.dataset.taskId, "taskId");
        this.returnUri = this.unStrToStr(this.root.dataset.taskReturnUri, "returnUri");
        this.requestUri = this.unStrToStr(this.root.dataset.taskUri, "taskUri");
        this.taskName = this.unStrToStr(this.root.dataset.taskName, "taskName");
        this.task = new Task((a) => "Not inited");
        this.task.onStateChange = (task: Task) => {
            let newElement = document.createElement("p");
            newElement.innerHTML = this.taskName + ": " + task.status;
            this.root.appendChild(newElement);
        };
    }

    public unStrToStr(input: string | undefined, paramName: string): string {
        if (input === undefined) {
            console.log(paramName + " undefined");
            console.log(this);
            return "undefined";
        }
        return input;
    }

    public async StartWorking() {
        while (true) {
            try {
                var a = await $.ajax({
                    accepts: { application: "application/json" },
                    method: "GET",
                    url: "/tasks/GetTaskToSolve",
                    async: true,
                    contentType: "application/json"
                });
                this.task = new Task(eval(a.javascriptCode));
                this.taskId = a.id;
                this.taskName = a.name;
                try {
                    await this.RunTests();
                } catch{ }
            } catch{ }
        }
    }
    private async RunTests() {
        var a = await $.ajax({
            accepts: { application: "application/json" },
            method: "GET",
            url: "/TaskInstances/GetTaskInstance/?taskId=" + this.taskId,
            async: true,
            contentType: "application/json"
        });
        while (true) {
            let curId = a.id;
            let inputParams = JSON.parse(a.inputParams);
            let res = encodeURI(JSON.stringify(this.task.run(inputParams)));
            a = await $.ajax({
                accepts: { application: "application/json" },
                method: "POST",
                url: "/TaskInstances/TaskInstanceSolved/" + curId + "/?result=" + res,
                data: JSON.stringify({
                    result: res
                }),
                async: true,
                contentType: "application/json"
            });
        }
    }
}