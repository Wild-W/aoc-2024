rows = open("4.txt").read().split("\n")
sum = 0
for i in range(len(rows) - 2):
    for j in range(len(rows[i]) - 2):
        if rows[i+1][j+1] == "A":
            if rows[i][j] == "M" and rows[i+2][j+2] == "S" \
                and ((rows[i+2][j] == "M" and rows[i][j+2] == "S") or (rows[i+2][j] == "S" and rows[i][j+2] == "M")): sum += 1
            elif rows[i][j] == "S" and rows[i+2][j+2] == "M" \
                and ((rows[i+2][j] == "M" and rows[i][j+2] == "S") or (rows[i+2][j] == "S" and rows[i][j+2] == "M")): sum += 1

print(sum)
