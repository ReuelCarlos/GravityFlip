using UnityEngine;

public class BoxNSwitch : MonoBehaviour
{
    
    public GameObject HeavyBox;
    public GameObject LightBox;
    private Rigidbody2D[] hbArray;
    private Rigidbody2D[] lbArray;
    private bool isSwitchFlipped = false;
    private int heavyBCount;
    private int lightBCount;
    //private Tag switchtag;

     //Interface
    public Collider2D player;
    private bool playerInRange = false;

    void Start()
    {
        heavyBCount = HeavyBox.transform.childCount;
        lightBCount = LightBox.transform.childCount;
        hbToArray();
        
    }

    
    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
           deciderSwitch();
        }        
    }

    void hbToArray(){
        
        hbArray = new Rigidbody2D[heavyBCount];
        for(int i = 0; i < heavyBCount; i++) 
        {
            hbArray[i] = HeavyBox.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>();
        }
    }

     //NOT FINAL FOR Box
    void OnTriggerEnter2D (Collider2D other)
    {

            playerInRange = true;
    }

    void OnTriggerExit2D (Collider2D other)
    {

        playerInRange = false;
    }


    void deciderSwitch()
    {
        if(gameObject.CompareTag("HeavySwitch")){
            heavyBox();
        }else if(gameObject.CompareTag("LightSwitch")){
            lightBox();
        }
    }

    void heavyBox()
    {
       isSwitchFlipped = !isSwitchFlipped; 
       

       if(isSwitchFlipped)
       {
            for(int i = 0; i < heavyBCount; i++) 
            {
                hbArray[i].gravityScale = -1f;
            }
       
       }else if(!isSwitchFlipped)
        {
            for(int i = 0; i < heavyBCount; i++) 
            {
                hbArray[i].gravityScale = 1f;
            }
        }
    }

    void lightBox()
    {
        /*isSwitchFlipped = !isSwitchFlipped; 
        if(isSwitchFlipped)
        {
         
            lightRB.gravityScale = -1f;
        
        }else if(!isSwitchFlipped)
         {
          
            lightRB.gravityScale = 1f;
         }*/
    }
    
}
