//polar to cartesian and cartesian to polar conversion function test project

//Note, this is not true polar coordinates as there is no distance component;
//intended for simple latitude+longitude conversions, ignoring altitude.
#pragma strict

var point:Vector3;
var polar:Vector2;
var eulerArrow:Transform;
var pointIndicator:Transform;


private var xStr:String;
private var yStr:String;
private var zStr:String;
private var latStr:String;
private var lonStr:String;


function Awake()
{
	xStr=point.x.ToString();
	yStr=point.y.ToString();
	zStr=point.z.ToString();
	latStr=polar.x.ToString();
	lonStr=polar.y.ToString();
}

function CartesianToPolar(point:Vector3):Vector2
{
	var polar:Vector2;
	//calc longitude 
	polar.y = Mathf.Atan2(point.x,point.z);
	
    //this is easier to write and read than sqrt(pow(x,2), pow(y,2))!
    var xzLen = Vector2(point.x,point.z).magnitude;
    //do the atan thing to get our latitude
    polar.x = Mathf.Atan2(-point.y,xzLen);

	//convert to degrees
	polar *= Mathf.Rad2Deg;
	
	return polar;
}

function PolarToCartesian(polar:Vector2):Vector3
{
	//an origin vector, representing lat,lon of 0,0. 
	var origin=Vector3(0,0,1);
	//generate a rotation quat based on polar's angle values
	var rotation = Quaternion.Euler(polar.x,polar.y,0);
	//rotate origin by rotation
	var point:Vector3=rotation*origin;
	
	return point;
}

function OnGUI()
{
Debug.Log(latStr + " " + lonStr);
	
   	xStr=GUI.TextField( Rect(10,Screen.height-40,50,30), xStr);	
	float.TryParse(xStr,point.x);
   	yStr=GUI.TextField( Rect(70,Screen.height-40,50,30), yStr);	
	float.TryParse(yStr,point.y);
   	zStr=GUI.TextField( Rect(130,Screen.height-40,50,30), zStr);	
	float.TryParse(zStr,point.z);

	//if the Apply Polar button is pressed...
	if (GUI.Button(Rect(190,Screen.height-40,100,30),"Apply to polar"))
	{
		//... set polar to the conversion of point
	 	polar=CartesianToPolar(point);
	 	latStr=polar.x.ToString();
	 	lonStr=polar.y.ToString();
	}

   	latStr=GUI.TextField( Rect(10,Screen.height-80,50,30), latStr);	
	float.TryParse(latStr,polar.x);
   	lonStr=GUI.TextField( Rect(70,Screen.height-80,50,30), lonStr);	
	float.TryParse(lonStr,polar.y);

	//If the Apply Point button is pressed... 
	if (GUI.Button(Rect(190,Screen.height-80,100,30),"Apply to point"))
	{
		//...set point to the conversion of polar
		point=PolarToCartesian(polar);
    	//*1.2 to scale it out a bit beyond the arrow tip, otherwise 
    	//it will not be visible
    	//point *= 1.2;
    	xStr=point.x.ToString();
    	yStr=point.y.ToString();
    	zStr=point.z.ToString();
    	
   	}

   		
	
}

function Update()
{
	//set the euler arrow object's rotation to our polar coordinates...
    eulerArrow.rotation=Quaternion.Euler(polar.x,polar.y,0);
    //Debug.Log(polar.x + " " + polar.y + " ");
	//... and set the point indicator's position to our point vector.
	pointIndicator.position=point;
}


