/* Includes ------------------------------------------------------------------*/
#include "main.h"
#include "usart.h"
#include "gpio.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */
#include <stdio.h>
#include <string.h>
int fputc(int ch,FILE *f);
int fgetc(FILE *f);
void Init8266(char* IP,char* PORT);
void A8266Tx(char* message);
char* A8266Rx();
