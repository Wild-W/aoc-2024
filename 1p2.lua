local inspect = require "lib.inspect"

local file = io.open("1.txt", "r")
if file == nil then error("Input file was not found") end

local list_1 = {}

local line = file:read("l")
local occurances = {}
while line ~= nil do
    local first, second = line:match("(%d+)   (%d+)")
    list_1[#list_1+1], second = tonumber(first), tonumber(second)
    if second == nil then error("Parsing failed") end

    occurances[second] = (occurances[second] or 0) + 1

    line = file:read("l")
end

local similarity = 0
for _, v in ipairs(list_1) do
    similarity = similarity + v * (occurances[v] or 0)
end

print(similarity)
