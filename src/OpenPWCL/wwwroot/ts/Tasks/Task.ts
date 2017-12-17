class Task{
    public status : "running" | "finished" | "thrown" | "notStarted" | "notDeffined";
    public func: (input: any) => any;
    public onDefined: (task: Task) => void; 
    public onStarted: (task: Task) => void; 
    public onFinished: (task: Task) => void; 
    public onError: (task: Task) => void; 
    public onStateChange: (task: Task) => void; 
    public constructor(func?: (input: any) => any) {
        this.status = "notDeffined";
        if (func !== undefined) {
            this.func = func;
            this.status = "notStarted";
            try {
                this.onDefined(this);
                this.onStateChange(this);
            } catch{

            }
        }
    }
    public run(input: any) : any | undefined {
        this.status = "running";
        try {
            this.onStateChange(this);
            this.onStarted(this);
        } catch{}

        try {
            var val = this.func(input);
            this.status = "finished";
            try {
                this.onStateChange(this);
                this.onFinished(this);
            } catch{}
            return val;
        }catch {
            this.status = "thrown";
            try {
                this.onStateChange(this);
                this.onError(this);
            } catch{ }
        }
        return undefined;
    }
}