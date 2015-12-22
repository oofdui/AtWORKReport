using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for mdTimeline
/// </summary>
public class mdTimeline
{
    private string _userUID="";
    public string UserUID
    {
        get { return _userUID; }
        set { _userUID = value; }
    }
    private string _userName="";
    public string UserName
    {
        get { return _userName; }
        set { _userName = value; }
    }
    private string _userPhoto="";
    public string UserPhoto
    {
        get { return _userPhoto; }
        set { _userPhoto = value; }
    }
    private string _name="";
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    private string _detail="";
    public string Detail
    {
        get { return _detail; }
        set { _detail = value; }
    }
    private string _startWhen="";
    public string StartWhen
    {
        get { return _startWhen; }
        set { _startWhen = value; }
    }
    private string _endWhen="";
    public string EndWhen
    {
        get { return _endWhen; }
        set { _endWhen = value; }
    }
    public mdTimeline()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void Clear()
    {
        _userUID = "";
        _userName = "";
        _userPhoto = "";
        _name = "";
        _detail = "";
        _startWhen = "";
        _endWhen = "";
    }
}