//Elmo
//Novemeber 5
//Maze 2

Console.WriteLine("Welcome to the maze game");
Console.WriteLine("Use the arrow keys to move reach the # to win");
Console.WriteLine("Avoid the % bad guys");
Console.WriteLine("Collect ^ coins 100 points each to open the gate");
Console.WriteLine("Press ESC to quit");
Console.WriteLine("Press any key to start");
Console.ReadKey(true);

string[] mapRows = File.ReadAllLines("map.txt");

Console.Clear();

for(int r=0; r<mapRows.Length; r++)
{
    Console.WriteLine(mapRows[r]);
}

int totalCoins=0;
(int,int)? firstBad=null;
(int,int)? secondBad=null;

for(int r=0; r<mapRows.Length; r++)
{
    for(int c=0; c<mapRows[r].Length; c++)
    {
        if(mapRows[r][c]=='^') totalCoins++;
        if(mapRows[r][c]=='%')
        {
            if(firstBad==null) firstBad=(r,c);
            else if(secondBad==null) secondBad=(r,c);
        }
    }
}

int playerR=1;
int playerC=1;
Console.SetCursorPosition(playerC,playerR);

int score=0;
int coinsCollected=0;
Random rnd=new Random();
bool playing=true;

do
{
    Console.SetCursorPosition(0,mapRows.Length);
    Console.Write("Score: "+score+"   Coins: "+coinsCollected+"/"+totalCoins+"   ");

    ConsoleKey key=Console.ReadKey(true).Key;
    int newRow=playerR;
    int newCol=playerC;
    
    switch(key)
    {
        case ConsoleKey.Escape:
            playing=false;
            break;
        case ConsoleKey.UpArrow:
            newRow--;
            break;
        case ConsoleKey.DownArrow:
            newRow++;
            break;
        case ConsoleKey.LeftArrow:
            newCol--;
            break;
        case ConsoleKey.RightArrow:
            newCol++;
            break;
    }
    
    if(newRow<0) newRow=0;
    if(newRow>=mapRows.Length) newRow=mapRows.Length-1;
    if(newCol<0) newCol=0;
    if(newCol>=mapRows[newRow].Length) newCol=mapRows[newRow].Length-1;
    
    if(mapRows[newRow][newCol]!='*')
    {
        playerR=newRow;
        playerC=newCol;
        Console.SetCursorPosition(playerC,playerR);
    }
    
    if(mapRows[playerR][playerC]=='^')
    {
        score=score+100;
        coinsCollected++;
        char[] arr=mapRows[playerR].ToCharArray();
        arr[playerC]=' ';
        mapRows[playerR]=new string(arr);
        Console.SetCursorPosition(playerC,playerR);
        Console.Write(' ');
        Console.SetCursorPosition(playerC,playerR);
    }
    
    if(mapRows[playerR][playerC]=='$')
    {
        score=score+200;
        char[] arr=mapRows[playerR].ToCharArray();
        arr[playerC]=' ';
        mapRows[playerR]=new string(arr);
        Console.SetCursorPosition(playerC,playerR);
        Console.Write(' ');
        Console.SetCursorPosition(playerC,playerR);
    }
    
    if(coinsCollected>=10)
    {
        for(int r=0; r<mapRows.Length; r++)
        {
            char[] arr=mapRows[r].ToCharArray();
            for(int c=0; c<arr.Length; c++)
            {
                if(arr[c]=='|')
                {
                    arr[c]=' ';
                    Console.SetCursorPosition(c,r);
                    Console.Write(' ');
                }
            }
            mapRows[r]=new string(arr);
        }
    }
    
    if(firstBad.HasValue)
    {
        int row=firstBad.Value.Item1;
        int col=firstBad.Value.Item2;
        
        Console.SetCursorPosition(col,row);
        Console.Write(' ');
        
        int dir=rnd.Next(4);
        int nr=row;
        int nc=col;
        
        if(dir==0) nr--;
        else if(dir==1) nr++;
        else if(dir==2) nc--;
        else if(dir==3) nc++;
        
        if(nr<0 || nr>=mapRows.Length)
        {
            nr=row;
            nc=col;
        }
        else if(nc<0 || nc>=mapRows[nr].Length)
        {
            nr=row;
            nc=col;
        }
        else if(mapRows[nr][nc]=='*')
        {
            nr=row;
            nc=col;
        }
        
        if(secondBad.HasValue && secondBad.Value.Item1==nr && secondBad.Value.Item2==nc)
        {
            nr=row;
            nc=col;
        }
        
        Console.SetCursorPosition(nc,nr);
        Console.Write('%');
        
        firstBad=(nr,nc);
    }
    
    if(secondBad.HasValue)
    {
        int row=secondBad.Value.Item1;
        int col=secondBad.Value.Item2;
        
        Console.SetCursorPosition(col,row);
        Console.Write(' ');
        
        int dir=rnd.Next(4);
        int nr=row;
        int nc=col;
        
        if(dir==0) nr--;
        else if(dir==1) nr++;
        else if(dir==2) nc--;
        else if(dir==3) nc++;
        
        if(nr<0 || nr>=mapRows.Length)
        {
            nr=row;
            nc=col;
        }
        else if(nc<0 || nc>=mapRows[nr].Length)
        {
            nr=row;
            nc=col;
        }
        else if(mapRows[nr][nc]=='*')
        {
            nr=row;
            nc=col;
        }
        
        if(firstBad.HasValue && firstBad.Value.Item1==nr && firstBad.Value.Item2==nc)
        {
            nr=row;
            nc=col;
        }
        
        Console.SetCursorPosition(nc,nr);
        Console.Write('%');
        
        secondBad=(nr,nc);
    }
    
    if((firstBad.HasValue && firstBad.Value.Item1==playerR && firstBad.Value.Item2==playerC) ||
       (secondBad.HasValue && secondBad.Value.Item1==playerR && secondBad.Value.Item2==playerC))
    {
        Console.Clear();
        Console.WriteLine("You were caught by a bad guy");
        Console.WriteLine("Final Score"+score);
        playing=false;
    }
    
    if(mapRows[playerR][playerC]=='#')
    {
        Console.Clear();
        Console.WriteLine("Congratulations you reached the treasure");
        Console.WriteLine("Score"+score);
        playing=false;
    }
    
} while(playing);