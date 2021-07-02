General Information:
*******************
1. All packages will be restored once solution is Build.
2. Database will be created automatically in the memory.
3. Database file can be found in the startup project i.e. Job.Manager.Api
4. For text unit test cases, there will be separate database file which can be found in the bin\debug folder of the test project.
5. To open database file, please use free tool i.e. DB Browser(SQLite). 
6. Unit Tests can be directly executed from Test Explore within visual studio.
7. For browser based testing, please use this documentation url for more info : https://documenter.getpostman.com/view/15729239/TzkzrzSo


Design Information:
******************
1. Solution is build in n-layered archiecture form to adhere SOLID principles.
2. Strategy pattern is used so that in future, different forms of jobs can be introduced.
3. Repository Pattern is used to reduce code redundancy and enable standard CRUD operations.
4. Asynchronous programming is used to avoid waiitng time for users.
5. Background services is used to process the job in the background.
6. For logging, Serilog is used along with NetCore internal logging mechanism.
7. To preserve data in memory, SQList is used.
8. Publish Subscriber technique is used to notify processing service whenever new job is added.