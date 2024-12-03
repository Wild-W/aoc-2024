#include <iostream>
#include <fstream>
#include <sstream>
using namespace std;

bool is_safe(string line) {
    stringstream ss(line);
    
    int num, prev;
    ss >> prev >> num;
    if (abs(num - prev) > 3) return false;
    
    if (prev > num) {
        prev = num;
        while (ss >> num) {
            if (prev <= num || prev - num > 3) return false;
            prev = num;
        }
    }
    else if (prev < num) {
        prev = num;
        while (ss >> num) {
            if (prev >= num || num - prev > 3) return false;
            prev = num;
        }
    }
    else return false;
    return true;
}

int main(void) {
    ifstream file("2.txt");
    string line;
    int safe_report_count = 0;
    while (getline(file, line)) {
        if (is_safe(line)) safe_report_count++;
    }
    cout << safe_report_count << endl;
    file.close();
}
