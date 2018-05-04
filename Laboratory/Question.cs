using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory
{
 
    class Question
    {
        public String questionDescription = "问题描述";
        public String ChoiceA = "选项A";
        public String ChoiceB = "选项B";  
        public String ChoiceC = "选项C";
        public String ChoiceD = "选项D";
        public String RightAnswer = "正确答案";
        public int Points = 10;
        public Question(){}
        public Question(String description, String A, String B, String C, String D, String right,int points) {
            this.ChoiceA = A;
            this.ChoiceB = B;
            this.ChoiceC = C;
            this.ChoiceD = D;
            this.questionDescription = description;
            this.RightAnswer = right;
            this.Points = points;
        }
    }
}
