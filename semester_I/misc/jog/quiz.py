from pathlib import Path
import random
import time
class Question:
    number = ""
    sub_num = ""
    answers = []
    correct_answer = ""
    category = ""
    question = ""
    lines = []
    
    def organize_answers(self):
        self.number = self.number.replace(" ", "")  
        self.number = self.number.replace(".", "")
        temp = self.number.split('/')
        self.number = temp[0]
        if len(temp) > 1:
            self.sub_num = temp[1]
        print(temp)
        for i in range(0, len(self.answers)):
            if self.answers[i][0] == "-":
                self.correct_answer = self.answers[i][1:]
                self.answers[i] = self.answers[i][1:]

    def print_question(self):
        print()
        print(f'{self.category}\n----------')
        print(f'{self.number}, {self.sub_num}')
        print(self.question)
        for answer in self.answers:
            print(answer)
        print(self.correct_answer)
        
    
filepath = Path('jog.txt')
 
lines = filepath.read_text().splitlines()  
category = ""
questions = []
question = Question()

for index, line in enumerate(lines):
    same_question = True
    if index + 1 < len(lines):
        next_line = lines[index + 1]
        if "-------" in next_line and len(next_line) < 25:
            category = line
        
        if len(line) > 0 and line[0].isdigit() and "." in line:
            if question.category != "":
                question.organize_answers()
                questions.append(question)
            question = Question()
            question.answers = []
            question.number = line
            question.category = category
            question.question = next_line
        if len(line) > 0 and \
                (line[0] == "a" \
                or line[0] == "b" \
                or line[0] == "c" \
                or line[0] == "d" \
                or line[:2] == "-a" \
                or line[:2] == "-b" \
                or line[:2] == "-c" \
                or line[:2] == "-d"):
            question.answers.append(line)


while True:
    # num = random.randint(1, 187)
    num = input("")
    # time.sleep(0.2)
    num_str = f'{num}'
    for question in reversed(questions):
        if question.sub_num == "" and num_str == question.number:
            question.print_question()
            break

        elif num_str == question.number:
            second = random.randint(0, 1000)
            if second % 2 == 0:
                sub_num_str = "A"
            elif second % 3 == 0:
                sub_num_str = "B"
            else:
                sub_num_str = ""    
                
            for x in questions:
                if x.number == num_str and x.sub_num == sub_num_str:
                    print('found it')
                    question.print_question()
                    break
 
