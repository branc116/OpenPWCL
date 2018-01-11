(a) => {
    let isHashOk = (n, hashedMessage) => {
        for (let i = 0; i < n; i++) {
            if (hashedMessage[i] != 0)
                return false;
        }
        return true;
    };
    let generateRandomNum = (n) => {
        let a = "";
        for (let i = 0; i < n; i++) {
            a += Math.floor((Math.random() * 10)).toString();
        }
        return a;
    }
    let isOk = false;
    while (!isOk) {
        let randNum = generateRandomNum(10);
        let hashedA = sha265(a);
    }

}