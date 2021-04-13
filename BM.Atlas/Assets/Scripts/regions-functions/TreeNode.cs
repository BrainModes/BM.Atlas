using System;
using System.Collections.Generic;
using UnityEngine;

// A class which is used to build trees made up of TreeNodes.
public class TreeNode
{
    // Some things every node needs to know itself.
    public string _key;
    public Dictionary<string, TreeNode> _children;
	public Color _rolloverColor;
	public Color _clickColor;
	public Color _defaultColor;
    public Vector3 _bestViewPosition;
    public float _bestFOVScale;
	public TreeNode _parent;
    public GameObject _gameObject;
    public List<Renderer> _fragments;

    public bool _isFunctionNode;
    public string _titleJsonKey;
    public string _bodyJsonKey;

    // This method makes a new TreeNode and adds it to this one as a child.
    public TreeNode Add(string key)
    {
        // Handle null field. Create a children and renderer List if needed.
        if (this._children == null)
        {
            this._children = new Dictionary<string, TreeNode>();
        }
        if (this._fragments == null)
        {
            this._fragments = new List<Renderer>();
        }

        // Test if the item already exists, and if it does, return that item.
        TreeNode dummy = new TreeNode();
        if (this._children.TryGetValue(key, out dummy))
        {
            return dummy;
        }

        TreeNode result = new TreeNode();

        // Store.
        result._fragments = new List<Renderer>();
        this._children[key] = result;
		result._key = key;
		result._parent = this;
        return result;
    }
    
    // Get one of the children nodes.
    public TreeNode Get(string value)
    {
        // Get individual child node.
        if (this._children == null)
        {
            return null;
        }
        TreeNode result;
        if (this._children.TryGetValue(value, out result))
        {
            return result;
        }
        return null;
    }

    // Search the tree for a key, return the node if found. Else return null.
    public static TreeNode SearchTree(string key, TreeNode node)
    {
        //Debug.Log("SearchTree start!");
        TreeNode current_node = node;

        if (key == current_node._key)
        {
            //Debug.Log("(key == current_node._key), returning current node");
            return current_node;
        }
        if (null == current_node._children)
        {
            //Debug.Log("current_node has no children and isn't the right key, setting null");
            return null;
        }
        if (node._children.TryGetValue(key, out current_node))
        {
            return current_node;
        } else
        {
            foreach (KeyValuePair<string, TreeNode> item in node._children)
            {
                //Debug.Log("foreach, child node examined: " + item.Value._key);
                current_node = SearchTree(key,item.Value);
                if (current_node != null && key == current_node._key)
                {
                    return current_node;
                }
            }  
        }        
        return current_node;
    }
}