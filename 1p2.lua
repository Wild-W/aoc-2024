local inspect = require "lib.inspect"

local file = io.open("1.txt", "r")
if file == nil then error("Input file was not found") end

local list_1 = {}
local list_2 = {}

local line = file:read("l")
while line ~= nil do
    local first, second = line:match("(%d+)   (%d+)")
    list_1[#list_1+1], list_2[#list_2+1] = tonumber(first), tonumber(second)
    line = file:read("l")
end

table.sort(list_1)
table.sort(list_2)

local occurances = {}
for _, v in ipairs(list_2) do
    if occurances[v] == nil then
        occurances[v] = 1
    else
        occurances[v] = occurances[v] + 1
    end
end

local similarity = 0
for _, v in ipairs(list_1) do
    similarity = similarity + v * (occurances[v] or 0)
end

print(similarity)
