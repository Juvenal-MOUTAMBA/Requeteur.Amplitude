       select  age as CodAge,mucocertstat.BKCLI.cli as IdClient,idext as NumeroMembre,tcli,mucocertstat.bktelcli.Num
       from mucocertstat.BKCLI  left OUTER join mucocertstat.bktelcli  on mucocertstat.BKCLI.cli= mucocertstat.bktelcli.cli      
       where age='00205'   and Num is not null
       order by  IdClient asc
    
       
