#include <iostream>
#include <fstream>
#include <sstream>
#include <vector>
#include <functional>
using namespace std;

const vector<int>* create_report(const string line) {
    stringstream ss(line);
    vector<int>* report = new vector<int>;

    int level;
    while (ss >> level) {
        report->push_back(level);
    }

    return report;
}

const function<bool(int,int)> decr = [](int current, int next) {
    int difference = current - next;
    return difference <= 3 && difference >= 1;
};

const function<bool(int,int)> incr = [](int current, int next) {
    int difference = next - current;
    return difference <= 3 && difference >= 1;
};

const bool is_safe(const vector<int>& report, bool problem_dampened = false) {
    const auto is_valid = report[0] > report[1] ? decr : incr;
    
    for (int i = 0; i < report.size() - 1; i++) {
        if (is_valid(report[i], report[i+1])) continue;
        if (problem_dampened) return false;

        vector<int> test_report = report;
        test_report.erase(test_report.begin() + i);
        if (is_safe(test_report, true)) return true;

        if (i != 0) {
            test_report = report;
            test_report.erase(test_report.begin() + i - 1);
            if (is_safe(test_report, true)) return true;
        }

        test_report = report;
        test_report.erase(test_report.begin() + i + 1);
        return is_safe(test_report, true);
    }

    return true;
}

int main(void) {
    ifstream file("2.txt");
    string line;
    int safe_report_count = 0;
    while (getline(file, line)) {
        auto report = create_report(line);
        if (is_safe(*report)) {
            safe_report_count++;
        }
    }
    cout << safe_report_count << endl;
    file.close();
}
