#include <stdio.h>
#include "A8266.h"

uint8_t message[] = "0"; 

int fputc(int ch,FILE *f){
	uint8_t temp[1]={ch};
	HAL_UART_Transmit(&huart1,temp,1,2);
	return ch;
}

int fgetc(FILE *f){
	uint8_t ch;
	HAL_UART_Receive(&huart1,(uint8_t*)&ch,1,0xFFFF);
	return ch;
}

void Init8266(char* IP,char* PORT){
//8266 Init
	
	printf("AT+RST\r\n");
		while(1){ 
			char tmp[11];
			scanf("%[^\r\n]",tmp);getchar();
			if(strcmp(tmp,"WIFI GOT IP")==0){break;}
		}
			printf("AT+CIPSTART=\"TCP\",\"%s\",%s\r\n",IP,PORT);
}

void A8266Tx(char* message){
	//8266 TCP发送 不能在中断用
	printf("AT+CIPSEND=%d\r\n",strlen(message)+2);
	HAL_Delay(500);
	printf("%s\r\n",message);
}

char* A8266Rx(){
	
}