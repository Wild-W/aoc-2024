import fs from 'node:fs';

console.log(
    [...fs.readFileSync("./3.txt").toString().matchAll(/mul\((\d{1,3}),(\d{1,3})\)/g)]
    .reduce((sum, match) => sum += match[1]*match[2], 0)
);
