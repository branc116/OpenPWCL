//import { TaskManager } from "./TaskManager";
//import * as $ from "jquery";

let manager: TaskManager;
$(document).ready(async (i) => {
    manager = new TaskManager($(".body-content").get(0));
    await manager.StartWorking();
})