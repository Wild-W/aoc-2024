import fs from 'node:fs';

let enabled = true;
let sum = 0;
[...fs.readFileSync("./3.txt").toString().matchAll(/(mul\(\d{1,3},\d{1,3}\))|(do\(\))|(don't\(\))/g)].forEach(match => {
    if (enabled) {
        if (match[0] == "don't()") enabled = false;
        else if (match[0].startsWith("mul")) {
            let digits = match[0].match(/mul\((\d{1,3}),(\d{1,3})\)/);
            sum += digits[1] * digits[2];
        }
    }
    else if (match[0] == "do()") enabled = true;
});
console.log(sum);
