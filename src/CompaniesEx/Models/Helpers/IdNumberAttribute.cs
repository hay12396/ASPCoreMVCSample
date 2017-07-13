using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
sealed public class IdNumberAttribute : ValidationAttribute
{
    // Internal field to hold the mask value. 
    readonly string _id;

    public string Id
    {
        get { return _id; }
    }

    public IdNumberAttribute(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }


    public override bool IsValid(object o)
    {
        string idNumber = (string)o;
        if (!System.Text.RegularExpressions.Regex.IsMatch(idNumber, @"^\d{5,9}$"))
            return false;

        if (idNumber.Length < 9)
        {
            while (idNumber.Length < 9)
            {
                idNumber = '0' + idNumber;
            }
        }
        int mone = 0;
        int incNum;
        for (int i = 0; i < 9; i++)
        {
            incNum = Convert.ToInt32(idNumber[i].ToString());
            incNum *= (i % 2) + 1;
            if (incNum > 9)
                incNum -= 9;
            mone += incNum;
        }

        if (mone % 10 == 0)
            return true;
        else
            return false;
    }


    public override string FormatErrorMessage(string name)
    {
        return String.Format(CultureInfo.CurrentCulture,
          ErrorMessageString, name, this.Id);
    }

}