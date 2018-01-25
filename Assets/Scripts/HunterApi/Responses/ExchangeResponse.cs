using System;

[System.Serializable]
public class ExchangeResponse{

	public int type;
	public int code;
	public string message;
	public TransactionHash transactionHash;

	public bool IsSuccessfull(){
		return message == "SUCCESS";
	}

}