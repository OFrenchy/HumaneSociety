
WHILE (SELECT COUNT(RoomID) FROM Rooms) < 15  
BEGIN  
   INSERT INTO Rooms VALUES ( (SELECT COUNT(RoomID) + 1 FROM Rooms), null);
END  


