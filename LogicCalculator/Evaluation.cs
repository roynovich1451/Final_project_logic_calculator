﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LogicCalculator
{
    internal class Evaluation
    {
        public bool is_valid { get; set; }
        private readonly List<Statement> statement_list;
        private readonly int current_line;

        public Evaluation(List<Statement> statement_list, string rule)
        {
            this.statement_list = statement_list;
            current_line = statement_list.Count - 1;
            is_valid = false;
            switch (rule)
            {
                case "Data":
                    Data();
                    break;

                case "Assumption":
                    is_valid = true;
                    return;

                case "MP":
                    MP();
                    break;

                case "Copy":
                    Copy();
                    break;

                case "MT":
                    MT();
                    break;

                case "PBC":
                    PBC();
                    break;

                case "LEM":
                    LEM();
                    break;

                case "∧e1":
                    And_Elimination_One();
                    break;

                case "∧e2":
                    And_Elimination_Two();
                    break;

                case "∨i1":
                    Or_Introduction_One();
                    break;

                case "∨i2":
                    Or_Introduction_Two();
                    break;

                case "∨e":
                    Or_Elimination();
                    break;

                case "∧i":
                    And_Introduction();
                    break;

                case "¬i":
                    Not_Introduction();
                    break;

                case "¬e":
                    Not_Elimination();
                    break;

                case "⊥e":
                    Contradiction_Elimination();
                    break;

                case "¬¬e":
                    Not_Not_Elimination();
                    break;

                case "¬¬i":
                    Not_Not_Introduction();
                    break;

                case "→i":
                    Arrow_Introduction();
                    break;
            }
        }

        private void Data()
        {
            int index = statement_list[0].expression.IndexOf("⊢");
            if (index != -1)
                statement_list[0].expression = statement_list[0].expression.Substring(0, index);
            foreach (string s in statement_list[0].expression.Split(','))
            {
                is_valid = (s == statement_list[current_line].expression);
                if (is_valid)
                    return;
            }
            MessageBox.Show("Error at row " + current_line + "\nData doesn't exist in the original expression");
        }

        private void Copy()
        {
            //TODO:add box checks
            int row = Get_Row(statement_list[current_line].first_segment);
            if (row == -1)
                return;
            is_valid = statement_list[row].expression == statement_list[current_line].expression;
            if (!is_valid)
                MessageBox.Show("Error at row " + current_line + "\nValues should be equal");
        }

        private void MP()
        {
            int first_row = Get_Row(statement_list[current_line].first_segment),
            second_row = Get_Row(statement_list[current_line].second_segment);
            if (first_row == -1 || second_row == -1)
                return;
            string first_expression = statement_list[first_row].expression;
            string second_expression = statement_list[second_row].expression;
            is_valid = (first_expression == second_expression + "→" + statement_list[current_line].expression)
                || (first_expression == second_expression + "→(" + statement_list[current_line].expression + ")")
                || (second_expression == first_expression + "→" + statement_list[current_line].expression)
                || (second_expression == first_expression + "→(" + statement_list[current_line].expression + ")");
            if (!is_valid)
            {
                MessageBox.Show("Error at row " + current_line + "\nMisuse of MP");
            }
        }

        private void MT()
        {
            int first_row = Get_Row(statement_list[current_line].first_segment),
            second_row = Get_Row(statement_list[current_line].second_segment), index;
            if (first_row == -1 || second_row == -1)
                return;
            string left_part, right_part,
                first_expression = statement_list[first_row].expression,
                second_expression = statement_list[second_row].expression,
                current_expression = statement_list[current_line].expression;
            index = first_expression.IndexOf("→");

            //if the first expression contains ->
            if (index != -1)
            {
                left_part = first_expression.Substring(0, index);
                right_part = first_expression.Substring(index + 1, first_expression.Length - (index + 1));
                if (second_expression != "~" + right_part && second_expression != "¬" + right_part)
                {
                    MessageBox.Show("Error at rows: " + first_row + "-" + second_row + "\n MT missing ¬");
                    is_valid = false;
                    return;
                }
            }
            else //check if the second expression contains ->
            {
                index = second_expression.IndexOf("→");
                if (index != -1)
                {
                    left_part = second_expression.Substring(0, index);
                    right_part = second_expression.Substring(index + 1, second_expression.Length - (index + 1));
                    if (first_expression != "~" + right_part && first_expression != "¬" + right_part)
                    {
                        MessageBox.Show("Error at rows: " + first_row + "-" + second_row + "\n MT missing ¬");
                        is_valid = false;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Error at row " + current_line + "\n MT was called without →");
                    is_valid = false;
                    return;
                }
            }

            is_valid = current_expression == "~" + left_part || current_expression == "¬" + left_part;

            if (!is_valid)
                MessageBox.Show("Error at row " + current_line + "\nMisuse of MT");
        }

        private void PBC()
        {
            List<int> rows = Get_Lines_From_Segment(statement_list[current_line].first_segment);
            is_valid = statement_list[rows[rows.Count - 1]].expression == "⊥";
            if (!is_valid)
            {
                MessageBox.Show("Error at row " + current_line + "\nMissing ⊥ at the previous line");
                return;
            }
            is_valid &= Check_If_Not(statement_list[current_line].expression, statement_list[rows[0]].expression);
            if (!is_valid)
                MessageBox.Show("Error at row " + current_line + "\nMisuse of PBC");
        }

        private void LEM()
        {
            string left_part, right_part, expression = statement_list[current_line].expression;
            int index = expression.IndexOf("V");
            if (index == -1)
                index = expression.IndexOf("∨");
            if (index == -1)
            {
                MessageBox.Show("Error at row " + current_line + "\nLEM without V or ∨");
                return;
            }
            left_part = expression.Substring(0, index);
            right_part = expression.Substring(index + 1, expression.Length - (index + 1));
            is_valid = Check_If_Not(left_part, right_part);
            if (!is_valid)
            {
                MessageBox.Show("Error at row " + current_line + "\nMisuse of LEM");
            }
        }

        private void And_Introduction()
        {
            int first_row = Get_Row(statement_list[current_line].first_segment),
            second_row = Get_Row(statement_list[current_line].second_segment);
            if (first_row == -1 || second_row == -1)
                return;
            string first = statement_list[first_row].expression;
            string second = statement_list[second_row].expression;
            is_valid = statement_list[current_line].expression == first + "^" + second ||
                statement_list[current_line].expression == first + "∧" + second ||
                statement_list[current_line].expression == second + "^" + first ||
                statement_list[current_line].expression == second + "∧" + first;
            if (!is_valid)
            {
                MessageBox.Show("Error at row " + current_line + "\nMisuse of And Introduction");
            }
        }

        private void And_Elimination_One()
        {
            int row = Get_Row(statement_list[current_line].first_segment);
            if (row == -1)
                return;
            string original_expression = statement_list[row].expression;
            is_valid = original_expression.Contains(statement_list[current_line].expression + "^")
             || original_expression.Contains(statement_list[current_line].expression + "∧")
             || original_expression.Contains("(" + statement_list[current_line].expression + ")^")
             || original_expression.Contains("(" + statement_list[current_line].expression + ")∧");
            if (!is_valid)
            {
                MessageBox.Show("Error at row " + current_line + "\nMisuse of And Elimination 1");
            }
        }

        private void And_Elimination_Two()
        {
            int row = Get_Row(statement_list[current_line].first_segment);
            if (row == -1)
                return;
            string original_expression = statement_list[row].expression;
            is_valid = original_expression.Contains("^" + statement_list[current_line].expression) ||
            original_expression.Contains("∧" + statement_list[current_line].expression) ||
            original_expression.Contains("^(" + statement_list[current_line].expression + ")") ||
            original_expression.Contains("∧(" + statement_list[current_line].expression + ")");

            if (!is_valid)
            {
                MessageBox.Show("Error at row " + current_line + "\nMisuse of And Elimination 2");
            }
        }

        private void Or_Elimination()
        {
            throw new NotImplementedException();
        }

        private void Or_Introduction_One()
        {
            int row = Get_Row(statement_list[current_line].first_segment);
            if (row == -1)
                return;

            is_valid = statement_list[current_line].expression.Contains(statement_list[row].expression + "∨")
                || statement_list[current_line].expression.Contains(statement_list[row].expression + "V");
            if (!is_valid)
            {
                MessageBox.Show("Error at row " + current_line + "\nMisuse of or1 introduction");
            }
        }

        private void Or_Introduction_Two()
        {
            int row = Get_Row(statement_list[current_line].first_segment);
            if (row == -1)
                return;
            is_valid = statement_list[current_line].expression.Contains("∨" + statement_list[row].expression)
                || statement_list[current_line].expression.Contains("V" + statement_list[row].expression);
            if (!is_valid)
            {
                MessageBox.Show("Error at row " + current_line + "\nMisuse of or2 introduction");
            }
        }

        private void Not_Introduction()
        {
            is_valid = statement_list[current_line - 1].expression == "⊥";
            if (!is_valid)
                MessageBox.Show("Error at row " + current_line + "\nMissing ⊥ at the previous row");
            is_valid &= Check_If_Not(statement_list[current_line - 2].expression, statement_list[current_line - 3].expression);
            if (!is_valid)
                MessageBox.Show("Error at row " + current_line + "\nMissuse of Not Introduction");
        }

        private void Not_Elimination()
        {
            is_valid = statement_list[current_line].expression == "⊥";
            if (!is_valid)
                MessageBox.Show("Error at row " + current_line + "\nMissing ⊥ at the current row");
            int first_row = Get_Lines_From_Segment(statement_list[current_line].first_segment)[0],
            second_row = Get_Lines_From_Segment(statement_list[current_line].first_segment)[1];
            is_valid &= Check_If_Not(statement_list[first_row].expression, statement_list[second_row].expression);
            if (!is_valid)
                MessageBox.Show("Error at row " + current_line + "\nMissuse of Not Elimination");
        }

        private void Contradiction_Elimination()
        {
            is_valid = statement_list[current_line - 1].expression == "⊥";
            if (!is_valid)
                MessageBox.Show("Error at row " + current_line + "\nMissing ⊥ at the previous row");
            int first_row = Get_Lines_From_Segment(statement_list[current_line].first_segment)[0],
            second_row = Get_Lines_From_Segment(statement_list[current_line].first_segment)[1];
            is_valid &= Check_If_Not(statement_list[first_row].expression, statement_list[second_row].expression);
            if (!is_valid)
                MessageBox.Show("Error at row " + current_line + "\nMissuse of Not Elimination");
        }

        private void Not_Not_Elimination()
        {
            int row = Get_Row(statement_list[current_line].first_segment);
            if (row == -1)
                return;
            string original_expression = statement_list[row].expression;
            is_valid = original_expression == "¬¬" + statement_list[current_line].expression
                || original_expression == "¬¬(" + statement_list[current_line].expression + ")"
                || original_expression == "~~" + statement_list[current_line].expression
                || original_expression == "~~(" + statement_list[current_line].expression + ")";
            if (!is_valid)
            {
                MessageBox.Show("Error at row " + current_line + "\nMisuse of Not Not Elimination");
            }
        }

        private void Not_Not_Introduction()
        {
            int row = Get_Row(statement_list[current_line].first_segment);
            if (row == -1)
                return;
            is_valid = statement_list[current_line].expression == "¬¬" + statement_list[row].expression
                || statement_list[current_line].expression == "~~" + statement_list[row].expression;
            if (!is_valid)
            {
                MessageBox.Show("Error at row " + current_line + "\nMisuse of Not Not Introduction");
            }
        }

        private void Arrow_Introduction()
        {
            List<int> lines = Get_Lines_From_Segment(statement_list[current_line].first_segment);
            int start_row = lines[0],
            end_row = lines[lines.Count - 1];

            //TODO: add box check
            if (!statement_list[current_line].expression.Contains("→"))
            {
                MessageBox.Show("Error at row " + current_line + "\nMissing → row");
                is_valid = false;
                return;
            }

            is_valid = statement_list[start_row].rule == "Assumption" &&
                    statement_list[current_line].expression == statement_list[start_row].expression + "→" + statement_list[end_row].expression
                    || statement_list[current_line].expression == "(" + statement_list[start_row].expression + ")→" + statement_list[end_row].expression
                    || statement_list[current_line].expression == statement_list[start_row].expression + "→(" + statement_list[end_row].expression + ")"
                    || statement_list[current_line].expression == "(" + statement_list[start_row].expression + ")→(" + statement_list[end_row].expression + ")";

            if (!is_valid)
            {
                MessageBox.Show("Error at row " + current_line + "\nMisuse of Arrow Introduction");
            }
        }

        #region UTILITY

        private int Get_Row(string s)
        {
            if (s.Contains("-"))
            {
                MessageBox.Show("Error at row " + current_line + "\nSegment contains '-' when it should not");
                return -1;
            }
            int ret = Int32.Parse(s);
            if (ret < 1)
            {
                MessageBox.Show("Error at row " + current_line + "\nLine number must be bigger than 0");
                return -1;
            }
            if (ret > statement_list.Count - 1)
            {
                MessageBox.Show("Error at row " + current_line + "\nLine number can't be bigger than current line number");
            }
            if (ret == current_line)
            {
                MessageBox.Show("Error at row " + current_line + "\nLine number provided is equal to current line");
                return -1;
            }
            return ret;
        }

        private List<int> Get_Lines_From_Segment(string seg)
        {
            List<int> ret = new List<int>();
            string[] spli = seg.Split(',');
            foreach (string s in spli)
            {
                int index = s.IndexOf("-");
                if (index != -1)
                {
                    int starting_number = (int)Char.GetNumericValue(s[index - 1]),
                        last_number = (int)Char.GetNumericValue(s[index + 1]);
                    ret.AddRange(Enumerable.Range(starting_number, last_number - starting_number + 1));
                }
                else
                    ret.Add(Int32.Parse(s));
            }
            return ret;
        }

        private bool Check_If_Not(string first, string second)
        {
            return first == "~" + second || first == "¬" + second || second == "~" + first || second == "¬" + first;
        }

        private void DisplayErrorMsg(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        #endregion UTILITY
    }
}