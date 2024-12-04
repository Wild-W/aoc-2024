rows = open("4.txt").read().split("\n")
sum = 0
num_rows = len(rows)
for i in range(num_rows):
    row = rows[i]

    row_len = len(row)
    for j in range(row_len):
        if row_len - j >= 4:
            if row[j] == "X" and row[j+1] == "M" and row[j+2] == "A" and row[j+3] == "S": sum += 1
            elif row[j] == "S" and row[j+1] == "A" and row[j+2] == "M" and row[j+3] == "X": sum += 1

        if i + 3 < num_rows:
            if row[j] == "X":
                if row_len - j >= 4 and rows[i+1][j+1] == "M" and rows[i+2][j+2] == "A" and rows[i+3][j+3] == "S": sum += 1
                if rows[i+1][j] == "M" and rows[i+2][j] == "A" and rows[i+3][j] == "S": sum += 1
            elif row[j] == "S":
                if row_len - j >= 4 and rows[i+1][j+1] == "A" and rows[i+2][j+2] == "M" and rows[i+3][j+3] == "X": sum += 1
                if rows[i+1][j] == "A" and rows[i+2][j] == "M" and rows[i+3][j] == "X": sum += 1
        if i - 3 >= 0 and row_len - j >= 4:
            if row[j] == "X" and rows[i-1][j+1] == "M" and rows[i-2][j+2] == "A" and rows[i-3][j+3] == "S": sum += 1
            elif row[j] == "S" and rows[i-1][j+1] == "A" and rows[i-2][j+2] == "M" and rows[i-3][j+3] == "X": sum += 1

print(sum)
