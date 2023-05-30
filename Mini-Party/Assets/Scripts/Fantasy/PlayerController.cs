using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

namespace Q
{ 
    public class PlayerController : MonoBehaviour , IPunObservable
    {
        // Start is called before the first frame update
        private PhotonView _pv;
        private Rigidbody _rb;
        private Vector3 _curpos;
        private Animator _anim;
        private float speed = 5;
        private Vector3 m_currentDirection;
        bool isJump;
        [SerializeField] private ItemType currentItem;
        public Vector3 offset;
        //[SerializeField] private TextMeshProUGUI tx;

        Vector3 lookDir;
        float h, v;

        public GameObject punch;
        IEnumerator boosterCoroutine;
        IEnumerator slowCoroutine;

        public Camera _camera;
        public Text itemText;

        void Awake()
        {
            itemText = GameObject.Find("ItemText").GetComponent<Text>();
            _pv = GetComponent<PhotonView>();
            _rb = GetComponent<Rigidbody>();
            _anim = GetComponent<Animator>();
        }

        private void Start()
        {
            if(_pv.IsMine)
            {
                _camera = Camera.main;
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (MiniGameManager.Instance.isGameClear) return;

            if (transform.position.y < -50)
            {
                transform.position = PhotonMiniGameManager.Instance.pos[Random.RandomRange(0, PhotonMiniGameManager.Instance.pos.Length)].position;
            }
            if (_pv.IsMine)
            {
                _camera.transform.position = transform.position + offset;
                if (Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow))
                {

                    var x = Input.GetAxisRaw("Vertical");
                    var z = Input.GetAxisRaw("Horizontal");
                    v = x;
                    h = z;
                lookDir = x * Vector3.forward + z * Vector3.right;
                transform.rotation = Quaternion.LookRotation(lookDir);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                else
                {
                    v = 0;
                    h = 0;
                }

                //var dir = new Vector2(h, v).normalized;
                //Debug.Log(dir.magnitude);
                //_rb.velocity = new Vector3(dir.x * speed, _rb.velocity.y, dir.y * speed);
                //var isMove = dir.magnitude == 1 ? true : false;
                var value = new Vector2(h, v).normalized;
                _anim.SetBool("isMove", value.magnitude == 1 ? true : false);
                //Rotate(h, v);
                
                if(!isJump && Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
                if(Input.GetKeyDown(KeyCode.LeftControl))
                    UseItem();
            }
            else if ((transform.position - _curpos).sqrMagnitude >= 100) transform.position = _curpos;
            else transform.position = Vector3.Lerp(transform.position, _curpos, Time.deltaTime * 10);
        }
        void Jump()
        {
            isJump = true;
            _rb.AddForce(new Vector3(0, 6, 0), mode: ForceMode.Impulse);

        }
        void Rotate(float h, float v)
        {
            dir = new Vector3(h, 0, v).normalized;
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0);
        }
        Vector3 dir;
        void UseItem()
        {
            if(currentItem != ItemType.NONE)
            {
                Debug.Log("item : " + currentItem);
                switch (currentItem)
                {
                    case ItemType.Booster:

                        if (boosterCoroutine != null)
                        {
                            StopCoroutine(boosterCoroutine);
                        }

                        boosterCoroutine = Co_Booster();
                        StartCoroutine(boosterCoroutine);
                        currentItem = ItemType.NONE;

                        if(_pv.IsMine)
                        itemText.text = "현재 아이템 : 없음";
                        break;
                    case ItemType.Punch:

                        //if (_pv.IsMine)
                        if(PhotonNetwork.IsMasterClient)
                        {
                            punch.gameObject.SetActive(true);
                        }

                        currentItem = ItemType.NONE;
                        if (_pv.IsMine)
                            itemText.text = "현재 아이템 : 없음";
                        break;
                    case ItemType.WaterBall:

                        if(_pv.IsMine)
                        {
                            var waterBall = PhotonNetwork.Instantiate("WaterBall", transform.position + new Vector3(0,1.5f, 0), Quaternion.identity);
                            waterBall.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * 10f, ForceMode.Impulse);
                        }

                        currentItem = ItemType.NONE;
                        if (_pv.IsMine)
                            itemText.text = "현재 아이템 : 없음";
                        break;

                    case ItemType.Slow:


                        if (_pv.IsMine)
                        {
                            //_pv.RPC("BB", RpcTarget.AllBufferedViaServer);
                            var slowObj = PhotonNetwork.Instantiate("SlowObject", transform.position + new Vector3(0,1,0), Quaternion.identity);
                            slowObj.GetComponent<Rigidbody>().AddForce((dir - transform.position).normalized * 1f, ForceMode.Impulse);
                        }
                        currentItem = ItemType.NONE;
                        if (_pv.IsMine)
                            itemText.text = "현재 아이템 : 없음";
                        break;

                }

            }
        }
        [PunRPC]
        public void Punch()
        {
            punch.gameObject.SetActive(true);
        }
        IEnumerator Co_Slow()
        {
            speed = 1.8f;
            Debug.Log("speed : " + speed);
            yield return new WaitForSeconds(4);
            speed = 5;
            Debug.Log("speed : " + speed);
        }
        IEnumerator Co_Booster()
        {
            speed = 9;
            Debug.Log("speed : " + speed);
            yield return new WaitForSeconds(4);
            speed = 5;
            Debug.Log("speed : " + speed);
        }
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
                //Vector3 pos = transform.localPosition;
                //stream.Serialize(ref pos);
            }
            else
            {
                _curpos = (Vector3)stream.ReceiveNext();
                //Vector3 pos = Vector3.zero;
                //stream.Serialize(ref pos);  // pos gets filled-in. must be used somewhere
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ground")
                isJump = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("item"))
            {
                string result = "";
                switch (other.GetComponent<MiniGameItem>().type)
                {
                    case ItemType.Booster:result = "부스터"; break;
                    case ItemType.Slow: result = "슬로우";break;
                    case ItemType.Punch: result = "펀치"; break;
                    case ItemType.WaterBall: result = "물풍선"; break;
                }
                if (_pv.IsMine)
                    itemText.text = "현재 아이템 : " + result;
                if (_pv.IsMine)
                {
                    //tx.text = other.GetComponent<MiniGameItem>().type.ToString();
                    currentItem = other.GetComponent<MiniGameItem>().type;
                    Debug.Log(currentItem);
                }
                if (PhotonNetwork.IsMasterClient)
                    PhotonNetwork.Destroy(other.gameObject);
            }
            else if(other.CompareTag("SlowObject"))
            {
                if (_pv.IsMine)
                {
                    if (!other.GetComponent<SlowItemObject>().isComplete) return;
                    if(slowCoroutine != null)
                    {
                        StopCoroutine(slowCoroutine);
                    }
                    slowCoroutine = Co_Slow();
                    StartCoroutine(slowCoroutine);
                }
                    PhotonNetwork.Destroy(other.gameObject);
            }
            else if(other.CompareTag("WaterExp"))
            {
                _rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
            else if(other.CompareTag("Punch"))
            {
                var dir = other.transform.position - transform.position;
                _rb.AddForce(dir * 15, ForceMode.Impulse);
            }
            else if(other.CompareTag("Trophy"))
            {
                if(_pv.IsMine)
                MiniGameManager.Instance.pv.RPC("Goal",RpcTarget.AllBufferedViaServer);
                
                if(_pv.IsMine)
                MiniGameManager.Instance.GoalCharacter();
            }
        }
    }
}
