using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class bgloop : MonoBehaviour {
 
 [Range(0f,1f)] //将rollSpeed在Inspector面板中设置成滑动条的样式
 public float rollSpeed = 1f;//滚动速度
 private float right; //右边界
 private float left; //左边界
 private float distance; //左右边界距离
 
 // Use this for initialization
 void Start () {
 //计算左右边界。Bounds是当图形的边界框
 SpriteRenderer sRender = GetComponent<SpriteRenderer>();
 right = transform.position.x + sRender.bounds.extents.x/3;
 left = transform.position.x - sRender.bounds.extents.x/3; 
 
 distance = right - left;//左右边界相减得到距离
 }
 
 // Update is called once per frame
 void Update () {
 //使背景图片向右移动
 transform.localPosition+= rollSpeed * Vector3.right * Time.deltaTime;
 
 //判断是否到达右边界
 while(transform.position.x > right)
 {
 //如果到达，将背景图片的位置向后（x轴方向）调整一个背景图片长度的距离
 transform.position -= new Vector3(distance, 0, 0);
 }
 }
}